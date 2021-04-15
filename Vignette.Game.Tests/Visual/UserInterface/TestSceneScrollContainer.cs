// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneScrollContainer : ThemeProvidedTestScene
    {
        public TestSceneScrollContainer()
        {
            Add(new ScrollContainer
            {
                Size = new Vector2(200),
                Child = new Box
                {
                    Size = new Vector2(500),
                }
            });

            Add(new ScrollContainer(Direction.Horizontal)
            {
                Y = 200,
                Size = new Vector2(200),
                Child = new Box
                {
                    Size = new Vector2(500),
                }
            });
        }
    }
}
