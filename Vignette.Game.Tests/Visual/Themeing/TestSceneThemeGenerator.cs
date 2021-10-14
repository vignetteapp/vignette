// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.Themeing
{
    public class TestSceneThemeGenerator : TestSceneThemeSlots
    {
        private readonly BasicColourPicker backgroundColour;
        private readonly BasicColourPicker foregroundColour;
        private readonly BasicColourPicker accentColour;

        public TestSceneThemeGenerator()
        {
            Add(new FillFlowContainer
            {
                AutoSizeAxes = Axes.X,
                RelativeSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 5),
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Margin = new MarginPadding { Right = 10 },
                Children = new Drawable[]
                {
                    new SpriteText
                    {
                        Text = "Background"
                    },
                    backgroundColour = new BasicColourPicker
                    {
                        Scale = new Vector2(0.5f),
                    },
                    new SpriteText
                    {
                        Text = "Foreground"
                    },
                    foregroundColour = new BasicColourPicker
                    {
                        Scale = new Vector2(0.5f),
                    },
                    new SpriteText
                    {
                        Text = "Accent"
                    },
                    accentColour = new BasicColourPicker
                    {
                        Scale = new Vector2(0.5f),
                    },
                }
            });

            backgroundColour.Current.ValueChanged += _ => updateTheme();
            foregroundColour.Current.ValueChanged += _ => updateTheme();
            accentColour.Current.ValueChanged += _ => updateTheme();

            updateTheme();
        }

        private void updateTheme()
            => Provider.Current.Value = Theme.From(accentColour.Current.Value);
    }
}
