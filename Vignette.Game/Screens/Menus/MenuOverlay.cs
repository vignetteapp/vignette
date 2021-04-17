// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Presentations.Menus;

namespace Vignette.Game.Screens.Menus
{
    public abstract class MenuOverlay : OverlayContainer
    {
        private readonly MenuView view;

        private readonly NavigationBar navigation;

        protected abstract IEnumerable<MenuSlide> GetTabs();

        public MenuOverlay()
        {
            view = new MenuView();

            navigation = new NavigationBar(view.Presentation);
            navigation.OnBack += () => Hide();

            foreach (var tab in GetTabs())
                navigation.TabControl.AddItem(tab);

            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Child = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Absolute, 200),
                    new Dimension(GridSizeMode.Distributed),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        navigation,
                        view,
                    }
                },
            };
        }

        protected override void PopIn()
        {
            this
                .ScaleTo(1.05f)
                .ScaleTo(1.0f, 200, Easing.OutQuint)
                .FadeIn(200, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this
                .ScaleTo(1.05f, 200, Easing.OutQuint)
                .FadeOut(200, Easing.OutQuint);
        }
    }
}
