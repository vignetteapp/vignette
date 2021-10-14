// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace Vignette.Game.Tests.Visual.Typesetting
{
    public abstract class FontUsageTestScene : VignetteTestScene
    {
        private readonly FillFlowContainer flow;

        public FontUsageTestScene()
        {
            Add(flow = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
            });
        }

        public void AddText(FontUsage font) => flow.Add(new SpriteText
        {
            Margin = new MarginPadding(20),
            Font = font.With(size: 32),
            Text = @"The quick fox jumps over the lazy dog.",
        });
    }
}
