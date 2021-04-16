// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class VignetteDropdownHeader : DropdownHeader
    {
        private readonly SpriteText label;

        protected override LocalisableString Label
        {
            get => label.Text;
            set => label.Text = value;
        }

        public VignetteDropdownHeader()
        {
            AutoSizeAxes = Axes.None;
            Foreground.AutoSizeAxes = Axes.None;
            Foreground.RelativeSizeAxes = Axes.Both;

            Height = 40;
            BackgroundColour = Colour4.Transparent;
            BackgroundColourHover = Colour4.Transparent;

            Child = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Distributed),
                    new Dimension(GridSizeMode.Absolute, 40),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        label = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = SegoeUI.SemiBold.With(size: 20),
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Icon = FluentSystemIcons.CaretDown24,
                            Size = new Vector2(14),
                        }
                    },
                }
            };
        }
    }
}
