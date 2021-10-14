// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationVerticalTabControl<T> : NavigationTabControl<T>
    {
        public NavigationVerticalTabControl()
        {
            TabContainer.Direction = FillDirection.Vertical;
            TabContainer.AllowMultiline = true;
            TabContainer.Spacing = new Vector2(0, 2);
        }

        protected abstract class NavigationVerticalTabItem : NavigationTabItem
        {
            public NavigationVerticalTabItem(T value)
                : base(value)
            {
                Height = 36;
                RelativeSizeAxes = Axes.X;

                Add(Highlight.With(d =>
                {
                    d.Anchor = Anchor.CentreLeft;
                    d.Origin = Anchor.CentreLeft;
                    d.RelativeSizeAxes = Axes.Y;
                    d.Size = new Vector2(4, 0);
                }));
            }

            protected override void OnActivated()
                => Highlight.ResizeHeightTo(0.4f, 200, Easing.OutQuint);

            protected override void OnDeactivated()
                => Highlight.ResizeHeightTo(0, 200, Easing.OutQuint);
        }
    }
}
