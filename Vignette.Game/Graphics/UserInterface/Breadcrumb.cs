// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class Breadcrumb<T> : TabControl<T>
    {
        public Breadcrumb()
        {
            Height = 36;
            SwitchTabOnRemove = false;
            TabContainer.Spacing = new Vector2(12, 0);

            Current.ValueChanged += e =>
            {
                foreach (var t in TabContainer.Children.OfType<BreadcrumbItem>())
                {
                    var tIndex = TabContainer.IndexOf(t);
                    var tabIndex = TabContainer.IndexOf(TabMap[e.NewValue]);

                    t.State = tIndex > tabIndex ? Visibility.Hidden : Visibility.Visible;
                    t.Chevron.FadeTo(tIndex >= tabIndex ? 0 : 1f, 200, Easing.OutQuint);
                }
            };
        }

        protected override Dropdown<T> CreateDropdown() => null;

        protected override TabItem<T> CreateTabItem(T value)
            => new BreadcrumbItem(value);

        protected class BreadcrumbItem : TabItem<T>, IStateful<Visibility>
        {
            public ThemableSpriteIcon Chevron { get; private set; }

            private ThemableSpriteText text;

            private Visibility state;

            public Visibility State
            {
                get => state;
                set
                {
                    if (value == state)
                        return;

                    state = value;

                    this.FadeTo(State == Visibility.Visible ? 1 : 0, 500, Easing.OutQuint);

                    StateChanged?.Invoke(State);
                }
            }

            public event Action<Visibility> StateChanged;

            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
                => Alpha == 1.0f && text.ReceivePositionalInputAt(screenSpacePos);

            public override bool HandleNonPositionalInput => State == Visibility.Visible;

            public override bool HandlePositionalInput => State == Visibility.Visible;

            public BreadcrumbItem(T value)
                : base(value)
            {
                Height = 36;
                Padding = new MarginPadding { Right = 12 };
                AutoSizeAxes = Axes.X;
                Children = new Drawable[]
                {
                    text = new ThemableSpriteText
                    {
                        Text = value.ToString(),
                        Font = SegoeUI.Regular.With(size: 18),
                        Colour = ThemeSlot.Gray130,
                        Margin = new MarginPadding { Vertical = 6 },
                    },
                    Chevron = new ThemableSpriteIcon
                    {
                        Y = -2,
                        Size = new Vector2(8),
                        Icon = FluentSystemIcons.ChevronRight12,
                        Alpha = 0,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreLeft,
                        Colour = ThemeSlot.Gray130,
                        Margin = new MarginPadding { Left = 10 },
                    },
                };
            }

            public override void Show() => State = Visibility.Visible;

            public override void Hide() => State = Visibility.Hidden;

            protected override void OnActivated()
            {
                text.Font = SegoeUI.SemiBold.With(size: 18);
                text.Colour = ThemeSlot.Gray190;
            }

            protected override void OnDeactivated()
            {
                text.Font = SegoeUI.Regular.With(size: 18);
                text.Colour = ThemeSlot.Gray130;
            }
        }
    }
}
