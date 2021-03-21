// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Screens.Main
{
    public class NavigationBrandButton : NavigationButton
    {
        protected new Sprite SpriteIcon => (Sprite)base.SpriteIcon;

        protected new FillFlowContainer SpriteText => (FillFlowContainer)base.SpriteText;

        public NavigationBrandButton()
        {
            Size = new Vector2(Toolbar.TOOLBAR_WIDTH_EXTENDED, Toolbar.TOOLBAR_WIDTH);
            Style = ButtonStyle.Override;
        }

        [BackgroundDependencyLoader]
        private void load(VignetteApplicationBase app, GameHost host, TextureStore textures)
        {
            SpriteIcon.Texture = textures.Get("branding");
            Action = () => host.OpenUrlExternally("https://github.com/vignette-project/vignette");

            if (app.IsDevelopmentBuild)
                SpriteText.Add(new Badge(FluentSharedColours.Red10, "Local"));

            if (VignetteApplicationBase.IsInsiderBuild)
                SpriteText.Add(new Badge(FluentSharedColours.GreenCyan10, "Insiders"));
        }

        protected override Drawable CreateIcon() => new ThemedSprite
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Scale = new Vector2(0.4f),
        };

        protected override Drawable CreateText() => new FillFlowContainer
        {
            Spacing = new Vector2(5, 0),
            Direction = FillDirection.Horizontal,
            AutoSizeAxes = Axes.X,
            RelativeSizeAxes = Axes.Y,
            Children = new Drawable[]
            {
                new ThemedSpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Font = SegoeUI.Bold.With(size: 16),
                    Text = "VIGNETTE",
                }
            }
        };

        private class Badge : Container
        {
            public Badge(Colour4 colour, string text)
            {
                AutoSizeAxes = Axes.Both;
                Masking = true;
                CornerRadius = 2.5f;
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = colour,
                    },
                    new SpriteText
                    {
                        Padding = new MarginPadding { Horizontal = 5, Vertical = 3 },
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = SegoeUI.SemiBold.With(size: 11),
                        Text = text.ToUpperInvariant(),
                    },
                };
            }
        }
    }
}
