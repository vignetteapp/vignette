// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Sekai.Audio;
using Vignette.Allocation;

namespace Vignette.Audio;

public sealed class AudioManager : IObjectPool<AudioBuffer>
{
    private const int max_buffer_size = 8192;
    private const int max_buffer_count = 500;
    private readonly AudioDevice device;
    private readonly ConcurrentBag<AudioBuffer> bufferPool = new();
    private readonly List<StreamingAudioController> controllers = new();

    internal AudioManager(AudioDevice device)
    {
        this.device = device;
    }

    /// <summary>
    /// Creates a new <see cref="IAudioController"/> for a <see cref="AudioStream"/>.
    /// </summary>
    /// <param name="stream">The audio stream to attach to the controller.</param>
    /// <returns>An audio controller.</returns>
    public IAudioController GetController(AudioStream stream)
    {
        return new StreamingAudioController(device.CreateSource(), stream, this);
    }

    internal void Update()
    {
        for (int i = 0; i < controllers.Count; i++)
        {
            controllers[i].Update();
        }
    }

    /// <summary>
    /// Returns an <see cref="IAudioController"/> back to the <see cref="AudioManager"/>.
    /// </summary>
    /// <param name="controller">The controller to return.</param>
    public void Return(IAudioController controller)
    {
        if (controller is not StreamingAudioController streaming)
        {
            return;
        }

        if (!controllers.Remove(streaming))
        {
            return;
        }

        streaming.Dispose();
    }

    AudioBuffer IObjectPool<AudioBuffer>.Get()
    {
        if (!bufferPool.TryTake(out var buffer))
        {
            buffer = device.CreateBuffer();
        }

        return buffer;
    }

    bool IObjectPool<AudioBuffer>.Return(AudioBuffer item)
    {
        if (bufferPool.Count >= max_buffer_count)
        {
            item.Dispose();
            return false;
        }

        bufferPool.Add(item);
        return true;
    }

    private sealed class StreamingAudioController : IAudioController, IDisposable
    {
        public bool Loop { get; set; }

        public TimeSpan Position
        {
            get => getTimeFromByteCount((int)stream.Position, stream.Format, stream.SampleRate);
            set => seek(getByteCountFromTime(value, stream.Format, stream.SampleRate));
        }

        public TimeSpan Duration => getTimeFromByteCount((int)stream.Length, stream.Format, stream.SampleRate);

        public TimeSpan Buffered => getTimeFromByteCount(buffered, stream.Format, stream.SampleRate);

        public AudioSourceState State => source.State;

        private int buffered;
        private bool isDisposed;
        private const int max_buffer_stream = 4;
        private readonly AudioSource source;
        private readonly AudioStream stream;
        private readonly IObjectPool<AudioBuffer> bufferPool;

        public StreamingAudioController(AudioSource source, AudioStream stream, IObjectPool<AudioBuffer> bufferPool)
        {
            this.source = source;
            this.stream = stream;
            this.bufferPool = bufferPool;
        }

        public void Play()
        {
            if (State != AudioSourceState.Paused)
            {
                seek(0);

                for (int i = 0; i < max_buffer_stream; i++)
                {
                    var buffer = bufferPool.Get();

                    if (!allocate(buffer))
                    {
                        break;
                    }

                    source.Enqueue(buffer);
                }
            }

            source.Play();
        }

        public void Stop()
        {
            seek(0);
        }

        public void Pause()
        {
            source.Pause();
        }

        public void Update()
        {
            while (source.TryDequeue(out var buffer))
            {
                if (!allocate(buffer))
                {
                    source.Loop = Loop;
                    break;
                }

                source.Enqueue(buffer);
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            source.Stop();

            while(source.TryDequeue(out var buffer))
            {
                bufferPool.Return(buffer);
            }

            source.Dispose();

            isDisposed = false;
        }

        private void seek(int position)
        {
            source.Stop();
            source.Clear();
            stream.Position = buffered = position;
        }

        private bool allocate(AudioBuffer buffer)
        {
            Span<byte> data = stackalloc byte[max_buffer_size];
            int read = stream.Read(data);

            if (read <= 0)
            {
                return false;
            }

            buffer.SetData<byte>(data[..read], stream.Format, stream.SampleRate);
            buffered += read;

            return true;
        }
    }

    private static int getChannelCount(AudioFormat format)
    {
        return format is AudioFormat.Stereo8 or AudioFormat.Stereo16 ? 2 : 1;
    }

    private static int getSamplesCount(AudioFormat format)
    {
        return format is AudioFormat.Stereo8 or AudioFormat.Mono8 ? 8 : 16;
    }

    private static int getByteCountFromTime(TimeSpan time, AudioFormat format, int sampleRate)
    {
        return (int)time.TotalSeconds * sampleRate * getChannelCount(format) * (getSamplesCount(format) / 8);
    }

    private static TimeSpan getTimeFromByteCount(int count, AudioFormat format, int sampleRate)
    {
        return TimeSpan.FromSeconds(count / (sampleRate * getChannelCount(format) * (getSamplesCount(format) / 8)));
    }
}
