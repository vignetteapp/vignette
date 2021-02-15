// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Shapes;

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
                    Direction = FillDirection.Horizontal,
                    Height = 50,
                    AutoSizeAxes = Axes.X,
                },
                new VignetteBox
                {
                    Size = new Vector2(50),
                    Colouring = Colouring.Accent,
                },
                new OutlinedBox
                {
                    Size = new Vector2(50),
                    Colouring = Colouring.Override,
                    BorderColour = Colour4.Red,
                    BorderThickness = 10.0f,
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

            for (int i = 0; i <= 10; i++)
                themedBoxFlow.Add(new VignetteBox { Size = new Vector2(50), Level = i, });
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
