// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentMenuHeader : MenuItem
    {
        public FluentMenuHeader(LocalisableString text)
            : base(text)
        {
        }
    }

    public class DrawableFluentMenuHeader : Menu.DrawableMenuItem
    {
        public DrawableFluentMenuHeader(MenuItem item)
            : base(item)
        {
        }

        protected override Drawable CreateBackground() => Empty();

        protected override Drawable CreateContent() => new HeaderMenuItem();

        protected override void UpdateBackgroundColour()
        {
        }

        protected override void UpdateForegroundColour()
        {
        }

        private class HeaderMenuItem : Container<ThemableSpriteText>, IHasText
        {
            public LocalisableString Text
            {
                get => Child.Text;
                set => Child.Text = value;
            }

            public HeaderMenuItem()
            {
                Height = 24;
                AutoSizeAxes = Axes.X;
                Child = new ThemableSpriteText
                {
                    Font = SegoeUI.SemiBold,
                    Colour = ThemeSlot.AccentPrimary,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Left = 8 },
                };
            }
        }
    }
}
