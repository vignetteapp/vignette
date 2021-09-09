// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentPopover : Popover
    {
        public FluentPopover()
        {
            Body.Margin = new MarginPadding(10);
            Body.Add(new ThemableEffectBox
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
                CornerRadius = 5.0f,
                BorderThickness = 1.5f,
                BorderColour = ThemeSlot.Gray110,
                Colour = ThemeSlot.White,
            });

            Background.Colour = Colour4.Transparent;
            Content.Padding = new MarginPadding(5);
        }

        protected override void PopIn() => this.FadeIn(200, Easing.OutQuint);

        protected override void PopOut() => this.FadeOut(200, Easing.OutQuint);

        protected override Drawable CreateArrow() => Empty();
    }
}
