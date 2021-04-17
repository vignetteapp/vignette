// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Presentations.Menus;

namespace Vignette.Game.Screens.Menus
{
    public class NavigationTabItem : TabItem<MenuSlide>
    {
        private readonly NavigationBarItem item;

        private readonly Box overlay;

        public NavigationTabItem(MenuSlide value)
            : base(value)
        {
            Height = 40;
            RelativeSizeAxes = Axes.X;

            Children = new Drawable[]
            {
                overlay = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                },
                item = new NavigationBarItem
                {
                    RelativeSizeAxes = Axes.X,
                    Icon = FluentSystemIcons.QuestionCircle24,
                    Text = value.GetType().Name,
                },
            };
        }

        private Bindable<Theme> theme;

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                overlay.Colour = e.NewValue.Black;
                item.Colour = e.NewValue.Black;
            }, true);
        }

        protected override void OnActivated()
        {
        }

        protected override void OnDeactivated()
        {
        }

        protected override bool OnHover(HoverEvent e)
        {
            overlay.Alpha = 0.1f;
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (!e.HasAnyButtonPressed)
                overlay.Alpha = 0;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            overlay.Alpha = 0.2f;
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);
            overlay.Alpha = ScreenSpaceDrawQuad.Contains(e.ScreenSpaceMousePosition) ? 0.1f : 0;
        }
    }
}
