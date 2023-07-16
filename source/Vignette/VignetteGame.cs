// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai;
using Vignette.Audio;
using Vignette.Content;
using Vignette.Graphics;

namespace Vignette;

public sealed class VignetteGame : Game
{
    private Window root = null!;
    private Camera camera = null!;
    private Renderer renderer = null!;
    private AudioManager audio = null!;
    private ContentManager content = null!;
    private ServiceLocator services = null!;

    public override void Load()
    {
        audio = new(Audio);
        content = new(Storage);
        content.Add(new ShaderLoader(), ".hlsl");
        content.Add(new TextureLoader(Graphics), ".png", ".jpg", ".jpeg", ".bmp", ".gif");

        renderer = new(Graphics);

        services = new();
        services.Add(audio);
        services.Add(content);

        root = new(services)
        {
            (camera = new Camera { ProjectionMode = CameraProjectionMode.OrthographicOffCenter })
        };
    }

    public override void Draw()
    {
        root.Draw(renderer);
    }

    public override void Update(TimeSpan elapsed)
    {
        camera.ViewSize = Window.Size;
        audio.Update();
        root.Update(elapsed);
    }

    public override void Unload()
    {
        root.Clear();
    }
}
