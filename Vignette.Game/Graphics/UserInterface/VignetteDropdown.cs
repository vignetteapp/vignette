// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class VignetteDropdown<T> : Dropdown<T>
    {
        protected override VignetteDropdownHeader CreateHeader()
            => new VignetteDropdownHeader();

        protected override DropdownMenu CreateMenu()
            => new VignetteDropdownMenu();

        protected class VignetteDropdownMenu : DropdownMenu
        {
            private Bindable<Theme> theme;

            public VignetteDropdownMenu()
            {
                MaskingContainer.CornerRadius = 5;
            }

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item)
                => new DrawableVignetteDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction)
                => new VignetteScrollContainer<Drawable>(direction);

            protected override Menu CreateSubMenu()
                => new VignetteMenu(Direction.Vertical);

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> theme)
            {
                this.theme = theme.GetBoundCopy();
                this.theme.BindValueChanged(e =>
                {
                    BackgroundColour = e.NewValue.White;
                }, true);
            }

            protected class DrawableVignetteDropdownMenuItem : DrawableDropdownMenuItem
            {
                private Bindable<Theme> theme;

                public DrawableVignetteDropdownMenuItem(MenuItem item)
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
                        ForegroundColourSelected = e.NewValue.Black;
                        BackgroundColourHover = e.NewValue.NeutralQuaternaryAlt;
                        BackgroundColourSelected = e.NewValue.NeutralQuaternary;
                    }, true);
                }

                protected override Drawable CreateContent() => new SpriteText
                {
                    Font = SegoeUI.Regular.With(size: 18),
                    Margin = new MarginPadding { Horizontal = 20, Vertical = 5 },
                };
            }
        }
    }
}
