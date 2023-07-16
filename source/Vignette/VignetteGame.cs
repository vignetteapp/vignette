// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai;
using Vignette.Graphics;

namespace Vignette;

public sealed class VignetteGame : Game
{
    private Renderer renderer = null!;
    private readonly Window root = new();

    public override void Load()
    {
        renderer = new(Graphics);
    }

    public override void Draw()
    {
        root.Draw(renderer);
    }

    public override void Update(TimeSpan elapsed)
    {
        root.Update(elapsed);
    }

    public override void Unload()
    {
        root.Clear();
    }
}
