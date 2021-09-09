// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneContextMenu : UserInterfaceTestScene
    {
        public TestSceneContextMenu()
        {
            Add(new BoxWithContextMenu());
        }

        private class BoxWithContextMenu : ThemableBox, IHasContextMenu
        {
            public MenuItem[] ContextMenuItems => new MenuItem[]
            {
                new FluentMenuItem("Hello World"),
                new FluentMenuItem("Add To Favorites", SegoeFluent.Heart),
                new FluentMenuItem("Move to Recycle Bin", SegoeFluent.Delete),
                new FluentMenuItem("Send Feedback", SegoeFluent.ChatBubblesQuestion),
                new FluentMenuDivider(),
                new FluentMenuHeader("Ridiculous"),
                new FluentMenuItem("Be Sent to Another World", SegoeFluent.Globe),
                new FluentMenuDivider(),
                new FluentMenuItem("File Actions")
                {
                    Items = new MenuItem[]
                    {
                        new FluentMenuHeader("Actions"),
                        new FluentMenuItem("Add To Recents"),
                        new FluentMenuItem("Show File in Explorer", SegoeFluent.WindowNew),
                    }
                }
            };

            public BoxWithContextMenu()
            {
                Size = new Vector2(100);
                Colour = ThemeSlot.AccentPrimary;
            }
        }
    }
}
