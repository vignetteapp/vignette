// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

namespace Vignette;

/// <summary>
/// The root of <see cref="Vignette"/>.
/// </summary>
public sealed class Window : World
{
    public override IServiceLocator Services { get; }

    internal Window(IServiceLocator services)
    {
        Services = services;
    }
}
