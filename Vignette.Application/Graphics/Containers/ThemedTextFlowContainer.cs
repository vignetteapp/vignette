// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Graphics.Containers
{
    public class ThemedTextFlowContainer : TextFlowContainer
    {
        protected override SpriteText CreateSpriteText() => new ThemedSpriteText();
    }
}
