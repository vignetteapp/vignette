// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;

namespace Vignette.Graphics;

/// <summary>
/// Render Group Flags.
/// </summary>
[Flags]
public enum RenderGroup : uint
{
    /// <summary>
    /// Render Group Default
    /// </summary>
    Default = 0,

    /// <summary>
    /// Render Group 1
    /// </summary>
    Group1 = 1 << 0,

    /// <summary>
    /// Render Group 2
    /// </summary>
    Group2 = 1 << 1,

    /// <summary>
    /// Render Group 3
    /// </summary>
    Group3 = 1 << 2,

    /// <summary>
    /// Render Group 4
    /// </summary>
    Group4 = 1 << 3,

    /// <summary>
    /// Render Group 5
    /// </summary>
    Group5 = 1 << 4,

    /// <summary>
    /// Render Group 6
    /// </summary>
    Group6 = 1 << 5,

    /// <summary>
    /// Render Group 7
    /// </summary>
    Group7 = 1 << 6,

    /// <summary>
    /// Render Group 8
    /// </summary>
    Group8 = 1 << 7,

    /// <summary>
    /// Render Group 9
    /// </summary>
    Group9 = 1 << 8,

    /// <summary>
    /// Render Group 10
    /// </summary>
    Group10 = 1 << 9,

    /// <summary>
    /// Render Group 11
    /// </summary>
    Group11 = 1 << 10,

    /// <summary>
    /// Render Group 12
    /// </summary>
    Group12 = 1 << 11,

    /// <summary>
    /// Render Group 13
    /// </summary>
    Group13 = 1 << 12,

    /// <summary>
    /// Render Group 14
    /// </summary>
    Group14 = 1 << 13,

    /// <summary>
    /// Render Group 15
    /// </summary>
    Group15 = 1 << 14,

    /// <summary>
    /// Render Group 16
    /// </summary>
    Group16 = 1 << 15,
}
