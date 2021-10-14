// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestScenePopover : ThemeProvidedTestScene
    {
        public TestScenePopover()
        {
            Add(new BoxWithPopover { Size = new Vector2(100) });
        }

        private class BoxWithPopover : ThemableBox, IHasPopover
        {
            public Popover GetPopover() => new FluentPopover
            {
                Children = new Drawable[]
                {
                    new ThemableEffectBox
                    {
                        RelativeSizeAxes = Axes.Both,
                        CornerRadius = 5.0f,
                        BorderThickness = 1.5f,
                        BorderColour = ThemeSlot.Gray110,
                        Colour = ThemeSlot.White,
                    },
                    new Container
                    {
                        AutoSizeAxes = Axes.Both,
                        Padding = new MarginPadding(10),
                        Child = new ThemableSpriteText
                        {
                            Text = "Hello World",
                            Colour = ThemeSlot.Black,
                        },
                    },
                },
            };

            protected override bool OnClick(ClickEvent e)
            {
                this.ShowPopover();
                return base.OnClick(e);
            }
        }
    }
}
