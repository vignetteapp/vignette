// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

namespace Vignette.Graphics;

/// <summary>
/// A fully realized renderable that can be drawn.
/// </summary>
public readonly struct RenderData
{
    /// <summary>
    /// The world.
    /// </summary>
    public IWorld World { get; }

    /// <summary>
    /// The projector.
    /// </summary>
    public IProjector Projector { get; }

    /// <summary>
    /// The renderable.
    /// </summary>
    public RenderObject Renderable { get; }

    public RenderData(IProjector projector, IWorld world, RenderObject renderable)
    {
        World = world;
        Projector = projector;
        Renderable = renderable;
    }
}
