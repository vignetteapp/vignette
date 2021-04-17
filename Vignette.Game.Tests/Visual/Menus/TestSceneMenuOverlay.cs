// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Utils;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Screens.Menus;
using Vignette.Game.Presentations.Menus;

namespace Vignette.Game.Tests.Visual.Menus
{
    public class TestSceneMenuOverlay : ThemeProvidedTestScene
    {
        private readonly MenuOverlay overlay;

        public TestSceneMenuOverlay()
        {
            Add(overlay = new TestMenuOverlay());
            AddStep("toggle", () => overlay.ToggleVisibility());
        }

        private class TestMenuOverlay : MenuOverlay
        {
            protected override IEnumerable<MenuSlide> GetTabs() => new MenuSlide[]
            {
                new TestSlide(),
                new TestSlide(true),
                new TestSlide(false, true),
                new TestSlide(true, true),
            };
        }

        private class TestSlide : MenuSlide
        {
            private bool makeHeader;

            private bool makeFooter;

            public TestSlide(bool header = false, bool footer = false)
            {
                makeHeader = header;
                makeFooter = footer;

                InternalChild = new SpriteText
                {
                    Colour = getRandomColour(),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = GetType().Name,
                    Font = SegoeUI.Black.With(size: 72),
                };
            }

            public override Drawable CreateHeader()
            {
                return !makeHeader
                    ? null
                    : new SpriteText
                    {
                        Text = "Header",
                        Font = SegoeUI.Black.With(size: 32),
                        Colour = getRandomColour(),
                    };
            }

            public override Drawable CreateFooter()
            {
                return !makeFooter
                    ? null
                    : new SpriteText
                    {
                        Text = "Footer",
                        Font = SegoeUI.Black.With(size: 32),
                        Colour = getRandomColour(),
                    };
            }

            private Colour4 getRandomColour()
                => new Colour4(RNG.NextSingle(), RNG.NextSingle(), RNG.NextSingle(), 1);
        }
    }
}
