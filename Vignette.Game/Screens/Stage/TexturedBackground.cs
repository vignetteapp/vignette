// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Vignette.Game.Screens.Stage
{
    public class TexturedBackground : FileAssociatedBackground
    {
        public override IEnumerable<string> Extensions => new[] { ".png", ".jpg", ".jpeg" };

        protected override Drawable CreateBackground(Stream stream)
        {
            try
            {
                return new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fill,
                    Texture = Texture.FromStream(stream),
                };
            }
            catch (TextureTooLargeForGLException)
            {
                //TODO: Warn user that the image is too big
                return Drawable.Empty();
            }
        }
    }
}
