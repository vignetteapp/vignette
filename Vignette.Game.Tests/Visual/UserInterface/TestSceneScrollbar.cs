// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneScrollbar : UserInterfaceTestScene
    {
        public TestSceneScrollbar()
        {
            Add(new FluentScrollContainer
            {
                Size = new Vector2(256),
                Child = new Box
                {
                    Size = new Vector2(512),
                    Colour = Colour4.Transparent,
                },
            });
        }
    }
}
