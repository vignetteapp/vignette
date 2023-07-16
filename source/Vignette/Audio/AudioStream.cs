// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.IO;
using Sekai.Audio;

namespace Vignette.Audio;

/// <summary>
/// A <see cref="Stream"/> containing pulse code modulation (PCM) audio data.
/// </summary>
public class AudioStream : Stream
{
    /// <summary>
    /// The audio stream's sample rate
    /// </summary>
    public int SampleRate { get; }

    /// <summary>
    /// The audio stream's format.
    /// </summary>
    public AudioFormat Format { get; }

    public override bool CanRead => stream.CanRead;

    public override bool CanSeek => stream.CanSeek;

    public override bool CanWrite => stream.CanWrite;

    public override long Length => stream.Length;

    public override long Position
    {
        get => stream.Position;
        set => stream.Position = value;
    }

    private bool isDisposed;
    private readonly MemoryStream stream;

    public AudioStream(byte[] buffer, AudioFormat format, int sampleRate)
        : this(buffer, true, format, sampleRate)
    {
    }

    public AudioStream(byte[] buffer, bool writable, AudioFormat format, int sampleRate)
    {
        Format = format;
        stream = new MemoryStream(buffer, writable);
        SampleRate = sampleRate;
    }

    public AudioStream(byte[] buffer, int index, int count, AudioFormat format, int sampleRate)
        : this(buffer, index, count, true, format, sampleRate)
    {
    }

    public AudioStream(byte[] buffer, int index, int count, bool writable, AudioFormat format, int sampleRate)
    {
        Format = format;
        stream = new MemoryStream(buffer, index, count, writable);
        SampleRate = sampleRate;
    }

    public AudioStream(int capacity, AudioFormat format, int sampleRate)
    {
        Format = format;
        stream = new MemoryStream(capacity);
        SampleRate = sampleRate;
    }

    public AudioStream(AudioFormat format, int sampleRate)
        : this(0, format, sampleRate)
    {
    }

    public override void Flush()
    {
        stream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return stream.Read(buffer, offset, count);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return stream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        stream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        stream.Write(buffer, offset, count);
    }

    protected override void Dispose(bool disposing)
    {
        if (isDisposed)
        {
            return;
        }

        stream.Dispose();

        isDisposed = true;
    }
}
