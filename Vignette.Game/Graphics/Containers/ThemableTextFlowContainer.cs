// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
