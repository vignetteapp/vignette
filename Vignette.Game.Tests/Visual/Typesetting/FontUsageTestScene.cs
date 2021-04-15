// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;

namespace Vignette.Game.Tests.Visual.Typesetting
{
    public abstract class FontUsageTestScene : TestScene
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
