// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

namespace Vignette.Rendering;

/// <summary>
/// An enumeration of default blending modes.
/// </summary>
public enum BlendMode
{
    /// <summary>
    /// Non-premultiplied alpha blending the source and destination colors.
    /// </summary>
    NonPremultiplied,

    /// <summary>
    /// Disabled blending.
    /// </summary>
    Disabled,

    /// <summary>
    /// Additive blending where the destination color is appended to the source color.
    /// </summary>
    Additive,

    /// <summary>
    /// Opaque blending where the source is overwritten by the destination.
    /// </summary>
    Opaque,

    /// <summary>
    /// Alpha blending where the source and destination are blended using alpha.
    /// </summary>
    AlphaBlend,
}
