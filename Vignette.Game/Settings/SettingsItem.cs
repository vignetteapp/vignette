// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Settings
{
    public class SettingsItem : CompositeDrawable, IFilterable
    {
        public IconUsage Icon
        {
            get => (IconDrawable as IHasIcon)?.Icon ?? default;
            set
            {
                if (IconDrawable == null)
                {
                    LabelContainer.Add(IconDrawable = CreateIcon());
                    labelFlow.Margin = new MarginPadding { Left = 38 };
                }

                if (IconDrawable is IHasIcon hasIcon)
                    hasIcon.Icon = value;
            }
        }

        public LocalisableString Label
        {
            get => (LabelDrawable as IHasText)?.Text ?? string.Empty;
            set
            {
                if (LabelDrawable == null)
                    labelFlow.Insert(-1, LabelDrawable = CreateLabel());

                if (LabelDrawable is IHasText hasText)
                    hasText.Text = value;
            }
        }

        public LocalisableString Description
        {
            set
            {
                if (DescriptionFlow == null)
                {
                    labelFlow.Add(DescriptionFlow = new ThemableTextFlowContainer(descriptionCreationParameters)
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                DescriptionFlow.Text = value.ToString();
            }
        }

        protected readonly Container Foreground;

        protected readonly Container LabelContainer;

        protected Drawable IconDrawable { get; private set; }

        protected Drawable LabelDrawable { get; private set; }

        protected ThemableTextFlowContainer DescriptionFlow { get; private set; }

        public bool FilteringActive { get; set; }

        public bool MatchingFilter
        {
            set => this.FadeTo(value ? 1 : 0, 200, Easing.OutQuint);
        }

        public IEnumerable<string> Keywords { get; set; }

        public virtual IEnumerable<string> FilterTerms => Keywords == null ? new[] { Label.ToString() } : new List<string>(Keywords) { Label.ToString() }.Where(s => !string.IsNullOrEmpty(s));

        private readonly FillFlowContainer labelFlow;

        public SettingsItem()
        {
            Masking = true;
            CornerRadius = 5.0f;

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            InternalChildren = new Drawable[]
            {
                CreateBackground(),
                LabelContainer = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Padding = new MarginPadding { Horizontal = 10 },
                    Height = 50,
                    Child = labelFlow = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                },
                Foreground = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = 10 },
                }
            };
        }

        protected virtual Drawable CreateBackground() => new ThemableBox
        {
            RelativeSizeAxes = Axes.Both,
            Colour = ThemeSlot.Gray30,
        };

        protected virtual Drawable CreateIcon() => new ThemableSpriteIcon
        {
            Size = new Vector2(16),
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
            Colour = ThemeSlot.Black,
            Margin = new MarginPadding { Left = 6 },
        };

        protected virtual Drawable CreateLabel() => new ThemableSpriteText
        {
            Font = SegoeUI.Regular.With(size: 14),
            Colour = ThemeSlot.Black,
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
        };

        private static void descriptionCreationParameters(CheapThemableSpriteText s)
        {
            s.Font = SegoeUI.Regular.With(size: 12);
            s.Colour = ThemeSlot.Gray110;
        }
    }
}
