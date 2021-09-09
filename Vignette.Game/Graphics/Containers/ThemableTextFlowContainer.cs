// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.Sprites;

namespace Vignette.Game.Graphics.Containers
{
    public class ThemableTextFlowContainer : TextFlowContainer<CheapThemableSpriteText>
    {
        public ThemableTextFlowContainer(Action<CheapThemableSpriteText> creationParameters)
            : base(creationParameters)
        {
        }

        public ThemableTextFlowContainer()
        {
        }

        protected override CheapThemableSpriteText CreateSpriteText() => new CheapThemableSpriteText();
    }
}
