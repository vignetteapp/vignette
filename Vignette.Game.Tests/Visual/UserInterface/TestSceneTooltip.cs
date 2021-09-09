// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneTooltip : UserInterfaceTestScene
    {
        public TestSceneTooltip()
        {
            Add(new TestBoxWithTooltip
            {
                Size = new Vector2(256),
                Colour = ThemeSlot.AccentPrimary,
            });
        }

        private class TestBoxWithTooltip : ThemableBox, IHasTooltip
        {
            public LocalisableString TooltipText => @"Hello World";
        }
    }
}
