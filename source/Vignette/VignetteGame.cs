// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Jint;
using Sekai;
using Vignette.Graphics;
using Vignette.Scripting;

namespace Vignette;

public sealed class VignetteGame : Game
{
    private readonly World root;
    private readonly Engine engine;
    private readonly Renderer renderer;

    public VignetteGame(VignetteGameOptions options)
        : base(options)
    {
        options.Engine.Strict = true;
        options.Engine.Modules.ModuleLoader = new ScriptModuleLoader(Storage);

        root = new World();
        engine = new Engine(options.Engine);
        renderer = new Renderer(Graphics);
    }

    protected override void Draw()
    {
        root.Draw(renderer);
    }

    protected override void Update(TimeSpan elapsed)
    {
        engine.Advanced.ProcessTasks();
        root.Update(elapsed);
    }

    protected override void Unload()
    {
        root.Clear();
    }
}
