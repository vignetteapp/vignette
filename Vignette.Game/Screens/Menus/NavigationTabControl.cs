// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Presentations;
using Vignette.Game.Presentations.Menus;

namespace Vignette.Game.Screens.Menus
{
    public class NavigationTabControl : SlideTabControl<MenuSlide>
    {
        private readonly Container highlight;

        public NavigationTabControl(Presentation presentation)
            : base(presentation)
        {
            TabContainer.AllowMultiline = true;
            TabContainer.RelativeSizeAxes = Axes.X;
            TabContainer.AutoSizeAxes = Axes.Y;

            AddInternal(highlight = new Container
            {
                X = -2,
                Size = new Vector2(5, 40),
                Masking = true,
                CornerRadius = 5,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
            });

            Current.BindValueChanged(_ => moveHighlightToSelected(), true);
        }

        private Bindable<Theme> theme;

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                highlight.Colour = e.NewValue.Black;
            }, true);
        }

        private bool hasInitialPosition;

        private void moveHighlightToSelected() => ScheduleAfterChildren(() =>
        {
            if (SelectedTab != null)
            {
                highlight.MoveToY(SelectedTab.DrawPosition.Y, !hasInitialPosition ? 0 : 200, Easing.OutQuint);
                hasInitialPosition = true;
            }
        });

        protected override Dropdown<MenuSlide> CreateDropdown()
            => null;

        protected override TabItem<MenuSlide> CreateTabItem(MenuSlide value)
            => new NavigationTabItem(value);
    }
}
