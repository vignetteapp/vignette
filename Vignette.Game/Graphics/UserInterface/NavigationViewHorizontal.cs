// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationViewHorizontal<T> : NavigationView<T>
    {
        public NavigationViewHorizontal()
        {
            Height = 44;
        }

        protected abstract class NavigationViewHorizontalTabItem : NavigationViewTabItem
        {
            protected override bool ForceTextMargin => false;

            protected override float TextLeftMargin => 30;

            public NavigationViewHorizontalTabItem(T value)
                : base(value)
            {
                AutoSizeAxes = Axes.X;
                RelativeSizeAxes = Axes.Y;

                LabelContainer.Add(Highlight.With(d =>
                {
                    d.Anchor = Anchor.BottomCentre;
                    d.Origin = Anchor.BottomCentre;
                    d.RelativeSizeAxes = Axes.X;
                }));
            }

            protected override void OnActivated()
                => Highlight.ResizeHeightTo(2, 200, Easing.OutQuint);

            protected override void OnDeactivated()
                => Highlight.ResizeHeightTo(0, 200, Easing.OutQuint);
        }
    }
}
