// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual.Themeing
{
    public class TestSceneThemeSlots : ThemeProvidedTestScene
    {
        private readonly FillFlowContainer flow;

        public TestSceneThemeSlots()
        {
            Add(new BasicScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = flow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 10),
                }
            });

            foreach (var slot in Enum.GetValues<ThemeSlot>())
            {
                flow.Add(new FillFlowContainer
                {
                    Size = new Vector2(200, 40),
                    Direction = FillDirection.Horizontal,
                    Children = new Drawable[]
                    {
                        new ThemableBox
                        {
                            Colour = slot,
                            Size = new Vector2(40)
                        },
                        new SpriteText
                        {
                            Text = slot.ToString(),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Margin = new MarginPadding { Left = 10 }
                        }
                    }
                });
            }
        }
    }
}
