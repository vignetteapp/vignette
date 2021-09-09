// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Containers
{
    public class FluentTooltipContainer : TooltipContainer
    {
        protected override ITooltip CreateTooltip() => new FluentTooltip();

        public class FluentTooltip : VisibilityContainer, ITooltip
        {
            private ThemableSpriteText text;

            public FluentTooltip()
            {
                Alpha = 0;
                AutoSizeAxes = Axes.Both;

                Children = new Drawable[]
                {
                    new ThemableEffectBox
                    {
                        Colour = ThemeSlot.Gray10,
                        BorderColour = ThemeSlot.Gray30,
                        CornerRadius = 2.5f,
                        BorderThickness = 1.5f,
                        RelativeSizeAxes = Axes.Both,
                    },
                    text = new ThemableSpriteText
                    {
                        Colour = ThemeSlot.Gray190,
                        Margin = new MarginPadding(5),
                    },
                };
            }

            public void SetContent(object content)
            {
                if (!(content is string contentString))
                    return;

                text.Text = contentString;
            }

            public void Move(Vector2 pos)
                => Position = pos;

            protected override void PopIn()
                => this.FadeIn(200, Easing.OutQuint);

            protected override void PopOut()
                => this.FadeOut(200, Easing.OutQuint);
        }
    }
}
