// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens.Menu
{
    public class GuideScreen : MenuScreen
    {
        public override LocalisableString Title => "Help";

        public override IconUsage Icon => FluentSystemIcons.Book24;
    }
}
