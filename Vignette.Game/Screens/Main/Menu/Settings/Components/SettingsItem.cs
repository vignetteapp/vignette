// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Main.Menu.Settings
{
    public abstract class SettingsItem : Container, IFilterable
    {
        private ThemableSpriteText label;

        public LocalisableString Label
        {
            get => label?.Text ?? string.Empty;
            set
            {
                if (label == null)
                {
                    flow.Insert(-1, label = new ThemableSpriteText
                    {
                        Font = SegoeUI.Regular.With(size: 16),
                        Colour = ThemeSlot.Gray190,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                label.Text = value;
            }
        }

        private ThemableSpriteText description;

        public LocalisableString Description
        {
            get => description?.Text ?? string.Empty;
            set
            {
                if (description == null)
                {
                    flow.Add(description = new ThemableSpriteText
                    {
                        Font = SegoeUI.Regular.With(size: 12),
                        Colour = ThemeSlot.Gray90,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                description.Text = value;
            }
        }

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1 : 0);
        }

        public bool FilteringActive { get; set; }

        public IEnumerable<string> Keywords { get; set; }

        public IEnumerable<string> FilterTerms => Keywords == null ? new[] { Label.ToString() } : new List<string>(Keywords) { Label.ToString() };

        private FillFlowContainer flow;

        protected Drawable Control { get; }

        public SettingsItem()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        flow = new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 30,
                            Direction = FillDirection.Vertical,
                        },
                        new Container
                        {
                            AutoSizeAxes = Axes.Both,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Child = Control = CreateControl(),
                        }
                    },
                },
                new ThemableBox
                {
                    Height = 2,
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Colour = ThemeSlot.Gray10,
                },
            };
        }

        protected abstract Drawable CreateControl();
    }

    public abstract class SettingsItem<T> : SettingsItem, IHasCurrentValue<T>
    {
        public Bindable<T> Current
        {
            get => controlWithCurrent.Current;
            set => controlWithCurrent.Current = value;
        }

        private IHasCurrentValue<T> controlWithCurrent => Control as IHasCurrentValue<T>;
    }
}
