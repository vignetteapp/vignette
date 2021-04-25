// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentMenuDivider : MenuItem
    {
        public FluentMenuDivider()
            : base(string.Empty)
        {
        }
    }

    public class DrawableFluentMenuDivider : FluentMenu.DrawableMenuItem
    {
        public DrawableFluentMenuDivider(MenuItem item)
            : base(item)
        {
            BackgroundColour = Colour4.White;
            BackgroundColourHover = Colour4.White;
        }

        protected override Drawable CreateBackground() => new ThemableBox
        {
            Height = 1.5f,
            Colour = ThemeSlot.Gray30,
            RelativeSizeAxes = Axes.X,
        };

        protected override Drawable CreateContent()
            => Drawable.Empty();

        protected override void UpdateBackgroundColour()
        {
        }

        protected override void UpdateForegroundColour()
        {
        }
    }
}
