// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Tests.Visual.Typesetting
{
    public class TestSceneSegoeUI : FontUsageTestScene
    {
        public TestSceneSegoeUI()
        {
            AddText(SegoeUI.Light);
            AddText(SegoeUI.SemiLight);
            AddText(SegoeUI.Regular);
            AddText(SegoeUI.SemiBold);
            AddText(SegoeUI.Bold);
            AddText(SegoeUI.Black);
        }
    }
}
