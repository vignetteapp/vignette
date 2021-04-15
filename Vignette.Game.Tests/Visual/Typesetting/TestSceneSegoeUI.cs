// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
