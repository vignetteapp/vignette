// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens.Menus
{
    public class NavigationBarItem : FillFlowContainer, IHasText
    {
        private readonly SpriteIcon icon;

        public IconUsage Icon
        {
            get => icon.Icon;
            set => icon.Icon = value;
        }

        private readonly SpriteText text;

        public LocalisableString Text
        {
            get => text.Text;
            set => text.Text = value;
        }

        public NavigationBarItem()
        {
            RelativeSizeAxes = Axes.Both;
            Direction = FillDirection.Horizontal;
            Children = new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(40),
                    Child = icon = new SpriteIcon
                    {
                        Size = new Vector2(24),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                },
                new Container
                {
                    Height = 40,
                    RelativeSizeAxes = Axes.X,
                    Child = text = new SpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Font = SegoeUI.Bold.With(size: 18),
                    }
                }
            };
        }
    }
}
