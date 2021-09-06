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
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class BreadcrumbControl<T> : TabControl<T>
    {
        public BreadcrumbControl()
        {
            SwitchTabOnRemove = false;
            TabContainer.Spacing = new Vector2(6, 0);

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

        protected override TabItem<T> CreateTabItem(T value) => new BreadcrumbItem(value);

        protected class BreadcrumbItem : TabItem<T>, IStateful<Visibility>
        {
            private Visibility state;

            public ThemableSpriteIcon Chevron { get; private set; }

            protected readonly ThemableSpriteText Text;

            protected readonly FillFlowContainer FlowContainer;

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
                => Alpha == 1.0f && Text.ReceivePositionalInputAt(screenSpacePos);

            public override bool HandleNonPositionalInput => State == Visibility.Visible;

            public override bool HandlePositionalInput => State == Visibility.Visible;

            public BreadcrumbItem(T value)
                : base(value)
            {
                RelativeSizeAxes = Axes.Y;
                AutoSizeAxes = Axes.X;
                Child = FlowContainer = new FillFlowContainer
                {
                    Spacing = new Vector2(6, 0),
                    Direction = FillDirection.Horizontal,
                    AutoSizeAxes = Axes.X,
                    RelativeSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        Text = new ThemableSpriteText
                        {
                            Text = value.ToString(),
                            Colour = ThemeSlot.Gray130,
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                        },
                        Chevron = new ThemableSpriteIcon
                        {
                            Y = -2,
                            Size = new Vector2(8),
                            Icon = SegoeFluent.ChevronRight,
                            Alpha = 0,
                            Colour = ThemeSlot.Gray130,
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                        },
                    }
                };
            }

            public override void Show() => State = Visibility.Visible;

            public override void Hide() => State = Visibility.Hidden;

            protected override void OnActivated()
            {
                Text.Font = SegoeUI.SemiBold.With(size: 16);
                Text.Colour = ThemeSlot.Gray190;
            }

            protected override void OnDeactivated()
            {
                Text.Font = SegoeUI.Regular.With(size: 16);
                Text.Colour = ThemeSlot.Gray130;
            }
        }
    }
}
