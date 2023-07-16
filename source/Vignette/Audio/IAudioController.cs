// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai.Audio;

namespace Vignette.Audio;

/// <summary>
/// Provides access to audio playback controls.
/// </summary>
public interface IAudioController
{
    /// <summary>
    /// Gets or sets whether audio playback should loop.
    /// </summary>
    bool Loop { get; set; }

    /// <summary>
    /// Gets or seeks the current playback position.
    /// </summary>
    TimeSpan Position { get; set; }

    /// <summary>
    /// Gets total playable duration.
    /// </summary>
    TimeSpan Duration { get; }

    /// <summary>
    /// Gets the duration of the buffered data.
    /// </summary>
    TimeSpan Buffered { get; }

    /// <summary>
    /// Gets the state of this audio controller.
    /// </summary>
    AudioSourceState State { get; }

    /// <summary>
    /// Starts audio playback.
    /// </summary>
    void Play();

    /// <summary>
    /// Stops audio playback.
    /// </summary>
    void Stop();

    /// <summary>
    /// Pauses audio playback.
    /// </summary>
    void Pause();
}
