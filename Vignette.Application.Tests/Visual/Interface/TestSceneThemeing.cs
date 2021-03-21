// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneThemeing : TestScene
    {
        private FillFlowContainer themedBoxFlow;

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new FillFlowContainer
            {
                Spacing = new Vector2(0, 10),
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    themedBoxFlow = new FillFlowContainer
                    {
                        Width = 250,
                        Direction = FillDirection.Full,
                        AutoSizeAxes = Axes.Y,
                    },
                    new ThemedSolidBox
                    {
                        Size = new Vector2(50),
                        ThemeColour = ThemeColour.ThemePrimary,
                    },
                    new ThemedOutlinedBox
                    {
                        Size = new Vector2(50),
                        ThemeColour = ThemeColour.ThemePrimary,
                        BorderThickness = 5.0f,
                    }
                }
            });

            foreach (var colour in Enum.GetValues<ThemeColour>())
                themedBoxFlow.Add(new ThemedSolidBox { Size = new Vector2(50), ThemeColour = colour, });
        }
    }
}
