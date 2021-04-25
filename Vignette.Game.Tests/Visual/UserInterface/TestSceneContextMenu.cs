// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneContextMenu : UserInterfaceTestScene
    {
        public TestSceneContextMenu()
        {
            Add(new FluentContextMenuContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new BoxWithContextMenu(),
            });
        }

        private class BoxWithContextMenu : ThemableBox, IHasContextMenu
        {
            public MenuItem[] ContextMenuItems => new MenuItem[]
            {
                new FluentMenuItem("Hello World"),
                new FluentMenuItem("Add To Favorites", FluentSystemIcons.Heart16),
                new FluentMenuItem("Move to Recycle Bin", FluentSystemIcons.Delete16),
                new FluentMenuItem("Send Feedback", FluentSystemIcons.ChatBubblesQuestion20),
                new FluentMenuDivider(),
                new FluentMenuHeader("Ridiculous"),
                new FluentMenuItem("Be Sent to Another World", FluentSystemIcons.Globe16),
                new FluentMenuDivider(),
                new FluentMenuItem("File Actions")
                {
                    Items = new MenuItem[]
                    {
                        new FluentMenuHeader("Actions"),
                        new FluentMenuItem("Add To Recents"),
                        new FluentMenuItem("Show File in Explorer", FluentSystemIcons.WindowNew24),
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
