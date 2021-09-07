// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Vignette.Game.Screens.Stage
{
    public class TexturedBackground : FileAssociatedBackground
    {
        protected override IEnumerable<string> Extensions => new[] { "png", "jpg", "jpeg" };

        private readonly Sprite sprite;

        public TexturedBackground()
        {
            InternalChild = sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
            };
        }

        protected override void OnFileChanged(Stream stream) => sprite.Texture = Texture.FromStream(stream);
    }
}
