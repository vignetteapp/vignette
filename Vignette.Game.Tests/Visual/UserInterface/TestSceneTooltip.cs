// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
