// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneThemeing : TestSceneInterface
    {
        private FillFlowContainer themedBoxFlow;

        [BackgroundDependencyLoader]
        private void load()
        {
            AddComponentRange(new Drawable[]
            {
                themedBoxFlow = new FillFlowContainer
                {
                    Width = 250,
                    Direction = FillDirection.Full,
                    AutoSizeAxes = Axes.Y,
                },
                new VignetteBox
                {
                    Size = new Vector2(50),
                    ThemeColour = ThemeColour.ThemePrimary,
                },
                new OutlinedBox
                {
                    Size = new Vector2(50),
                    ThemeColour = ThemeColour.ThemePrimary,
                    BorderThickness = 5.0f,
                },
                new FillFlowContainer
                {
                    Height = 100,
                    AutoSizeAxes = Axes.X,
                    Spacing = new Vector2(20, 0),
                    Children = new[]
                    {
                        new ElevatedContainer(1),
                        new ElevatedContainer(2),
                        new ElevatedContainer(3),
                    }
                },
            });

            foreach (var colour in Enum.GetValues<ThemeColour>())
                themedBoxFlow.Add(new VignetteBox { Size = new Vector2(50), ThemeColour = colour, });
        }

        private class ElevatedContainer : Container
        {
            public ElevatedContainer(int elevation)
            {
                Masking = true;
                Size = new Vector2(100);
                Child = new VignetteBox { RelativeSizeAxes = Axes.Both };

                switch (elevation)
                {
                    case 1:
                        EdgeEffect = VignetteStyle.ElevationOne;
                        break;

                    case 2:
                        EdgeEffect = VignetteStyle.ElevationTwo;
                        break;

                    case 3:
                        EdgeEffect = VignetteStyle.ElevationThree;
                        break;
                }
            }
        }
    }
}
