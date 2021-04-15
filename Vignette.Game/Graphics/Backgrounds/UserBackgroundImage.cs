// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using Vignette.Game.IO;

namespace Vignette.Game.Graphics.Backgrounds
{
    public class UserBackgroundImage : UserBackground
    {
        private readonly string textureName;

        private readonly Sprite sprite;

        public UserBackgroundImage(string textureName = @"")
        {
            this.textureName = textureName;

            InternalChild = sprite = new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
            };
        }

        [BackgroundDependencyLoader]
        private void load(UserResources resources)
        {
            sprite.Texture = resources.Images.Get(textureName) ?? Texture.WhitePixel;
        }
    }
}
