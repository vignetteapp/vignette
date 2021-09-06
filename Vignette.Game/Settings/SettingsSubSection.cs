// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.LocalisationExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Settings
{
    public class SettingsSubSection : Container, IHasFilterableChildren
    {
        public LocalisableString Label
        {
            get => label?.Text ?? string.Empty;
            set
            {
                if (label == null)
                {
                    AddInternal(label = new ThemableSpriteText
                    {
                        Font = SegoeUI.SemiBold.With(size: 12),
                        Colour = ThemeSlot.Gray60,
                    });

                    flow.Margin = new MarginPadding { Top = 20 };
                }

                label.Text = value;
            }
        }

        protected override Container<Drawable> Content => flow;

        public IEnumerable<IFilterable> FilterableChildren => flow.OfType<IFilterable>();

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1 : 0, 200, Easing.OutQuint);
        }

        public bool FilteringActive { get; set; }

        public IEnumerable<string> FilterTerms => new[] { Label.ToString() };

        private readonly FillFlowContainer flow;
        private ThemableSpriteText label;

        public SettingsSubSection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            InternalChildren = new Drawable[]
            {
                flow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 5),
                }
            };
        }
    }
}
