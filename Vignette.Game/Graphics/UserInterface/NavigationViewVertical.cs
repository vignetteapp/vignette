// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationViewVertical<T> : NavigationView<T>
    {
        public NavigationViewVertical()
        {
            TabContainer.Direction = FillDirection.Vertical;
            TabContainer.AllowMultiline = true;
        }

        protected abstract class NavigationViewVerticalTabItem : NavigationViewTabItem
        {
            public NavigationViewVerticalTabItem(T value)
                : base(value)
            {
                Height = 44;
                RelativeSizeAxes = Axes.X;

                Add(Highlight.With(d =>
                {
                    d.Height = 0.75f;
                    d.Anchor = Anchor.CentreLeft;
                    d.Origin = Anchor.CentreLeft;
                    d.RelativeSizeAxes = Axes.Y;
                }));
            }

            protected override void OnActivated()
                => Highlight.ResizeWidthTo(2, 200, Easing.OutQuint);

            protected override void OnDeactivated()
                => Highlight.ResizeWidthTo(0, 200, Easing.OutQuint);
        }
    }
}
