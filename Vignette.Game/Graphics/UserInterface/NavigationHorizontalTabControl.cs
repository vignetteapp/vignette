// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationHorizontalTabControl<T> : NavigationTabControl<T>
    {
        public NavigationHorizontalTabControl()
        {
            Height = 44;
        }

        protected abstract class NavigationHorizontalTabItem : NavigationTabItem
        {
            protected override bool ForceTextMargin => false;

            protected override float TextLeftMargin => 30;

            public NavigationHorizontalTabItem(T value)
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
                => Highlight.ResizeHeightTo(3, 200, Easing.OutQuint);

            protected override void OnDeactivated()
                => Highlight.ResizeHeightTo(0, 200, Easing.OutQuint);
        }
    }
}
