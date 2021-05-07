// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Settings
{
    public abstract class SettingsSection : Container, IHasFilterableChildren
    {
        public abstract LocalisableString Header { get; }

        protected override Container<Drawable> Content => flow;

        private readonly ThemableMaskedBox background;

        private readonly FillFlowContainer flow;

        public IEnumerable<IFilterable> FilterableChildren => Children.OfType<IFilterable>();

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1 : 0);
        }

        public bool FilteringActive { get; set; }

        public IEnumerable<string> FilterTerms => new[] { Header.ToString() };

        public SettingsSection()
        {
            Margin = new MarginPadding { Top = 10 };
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            InternalChildren = new Drawable[]
            {
                background = new ThemableMaskedBox
                {
                    Alpha = 0,
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.Transparent,
                    BorderColour = ThemeSlot.Gray190,
                    BorderThickness = 1.5f,
                },
                flow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding(20),
                    Direction = FillDirection.Vertical,
                    Child = new ThemableSpriteText
                    {
                        Colour = ThemeSlot.Gray190,
                        Margin = new MarginPadding { Bottom = 10 },
                        Font = SegoeUI.SemiBold.With(size: 20),
                        Text = Header,
                    },
                }
            };
        }

        public void Highlight()
        {
            background
                .FadeInFromZero()
                .FadeOutFromOne(1000, Easing.OutQuint);
        }
    }
}
