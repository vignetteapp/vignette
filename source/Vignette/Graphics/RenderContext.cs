// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using Vignette.Scripting;

namespace Vignette.Graphics;

/// <summary>
/// A rendering context for a given <see cref="Drawable"/>.
/// </summary>
public readonly struct RenderContext
{
    private readonly ISpatialObject world;
    private readonly RenderQueue queue;
    private readonly IProjector projector;

    internal RenderContext(RenderQueue queue, IProjector projector, ISpatialObject world)
    {
        this.queue = queue;
        this.world = world;
        this.projector = projector;
    }

    /// <summary>
    /// Draws a render object.
    /// </summary>
    /// <param name="renderObject">The <see cref="RenderObject"/> to draw.</param>
    [ScriptVisible]
    public void Draw(RenderObject renderObject)
    {
        queue.Enqueue(projector, world, renderObject);
    }
}
