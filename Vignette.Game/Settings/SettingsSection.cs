// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.LocalisationExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Shapes;

namespace Vignette.Game.Settings
{
    public abstract class SettingsSection : Container, IHasFilterableChildren
    {
        public virtual IconUsage Icon { get; }

        public virtual LocalisableString Label { get; }

        protected override Container<Drawable> Content => flow;

        public IEnumerable<IFilterable> FilterableChildren => flow.OfType<IFilterable>();

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1 : 0, 200, Easing.OutQuint);
        }

        public bool FilteringActive { get; set; }

        public IEnumerable<string> FilterTerms => new[] { Label.ToString() };

        private readonly FillFlowContainer flow;
        private readonly Container body;

        public SettingsSection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            InternalChildren = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.Gray20,
                },
                body = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding(10),
                    Child = flow = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(0, 20),
                    }
                },
            };

            if (Label != default)
            {
                flow.Margin = new MarginPadding { Top = 30 };
                body.Add(new ThemableSpriteText
                {
                    Text = Label.ToUpper(),
                    Font = SegoeUI.SemiBold.With(size: 16),
                    Colour = ThemeSlot.Black,
                });
            }
        }
    }
}
