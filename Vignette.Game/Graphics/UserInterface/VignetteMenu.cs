// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class VignetteMenu : Menu
    {
        private Bindable<Theme> theme;

        public VignetteMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            MaskingContainer.CornerRadius = 5;
        }

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                BackgroundColour = e.NewValue.White;
            }, true);
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item)
            => new DrawableVignetteMenuItem(item);

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction)
            => new VignetteScrollContainer<Drawable>(direction);

        protected override Menu CreateSubMenu()
            => new VignetteMenu(Direction.Vertical);

        protected class DrawableVignetteMenuItem : DrawableMenuItem
        {
            private Bindable<Theme> theme;

            public DrawableVignetteMenuItem(MenuItem item)
                : base(item)
            {
                BackgroundColour = Colour4.Transparent;
            }

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> theme)
            {
                this.theme = theme.GetBoundCopy();
                this.theme.BindValueChanged(e =>
                {
                    ForegroundColour = e.NewValue.Black;
                    ForegroundColourHover = e.NewValue.Black;
                    BackgroundColourHover = e.NewValue.NeutralQuaternaryAlt;
                }, true);
            }

            protected override Drawable CreateContent() => new TextContainer();

            private class TextContainer : Container, IHasText
            {
                private readonly SpriteText label;

                public LocalisableString Text
                {
                    get => label.Text;
                    set => label.Text = value;
                }

                public TextContainer()
                {
                    Anchor = Anchor.CentreLeft;
                    Origin = Anchor.CentreLeft;
                    AutoSizeAxes = Axes.Both;
                    Child = label = new SpriteText
                    {
                        Font = SegoeUI.Regular.With(size: 18),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Horizontal = 10, Vertical = 5 },
                    };
                }
            }
        }
    }
}
