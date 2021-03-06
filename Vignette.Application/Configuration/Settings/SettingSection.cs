// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Configuration.Settings
{
    public abstract class SettingSection : Container
    {
        public abstract string Label { get; }

        public abstract IconUsage Icon { get; }

        private readonly FillFlowContainer flowContent;

        protected override Container<Drawable> Content => flowContent;

        public SettingSection()
        {
            Padding = new MarginPadding { Top = 10 };

            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Depth = float.MaxValue,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            Height = 30,
                            Margin = new MarginPadding { Bottom = 10 },
                            Direction = FillDirection.Horizontal,
                            RelativeSizeAxes = Axes.X,
                            Children = new Drawable[]
                            {
                                new VignetteSpriteIcon
                                {
                                    Size = new Vector2(22),
                                    Icon = Icon,
                                    Margin = new MarginPadding { Horizontal = 10 },
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    ThemeColour = ThemeColour.ThemePrimary,
                                },
                                new VignetteSpriteText
                                {
                                    Font = VignetteFont.Bold.With(size: 18),
                                    Text = Label,
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                }
                            },
                        },
                        new VignetteBox
                        {
                            Height = 1,
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            ThemeColour = ThemeColour.NeutralQuaternary,
                            RelativeSizeAxes = Axes.X,
                        },
                    },
                },
                flowContent = new FillFlowContainer
                {
                    Margin = new MarginPadding { Top = 50 },
                    Spacing = new Vector2(0, 10),
                    Direction = FillDirection.Vertical,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                },
            };
        }
    }
}
