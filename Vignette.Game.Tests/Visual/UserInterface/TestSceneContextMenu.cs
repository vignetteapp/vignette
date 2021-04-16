// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Cursor;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneContextMenu : ThemeProvidedTestScene
    {
        public TestSceneContextMenu()
        {
            Add(new VignetteContextMenuContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new BoxWithContextMenu
                {
                    Size = new Vector2(100),
                }
            });
        }

        private class BoxWithContextMenu : Box, IHasContextMenu
        {
            public MenuItem[] ContextMenuItems => new[]
            {
                new VignetteMenuItem("One"),
                new VignetteMenuItem("Two"),
                new VignetteMenuItem("Three"),
                new VignetteMenuItem("Really Long Menu Item"),
            };
        }
    }
}
