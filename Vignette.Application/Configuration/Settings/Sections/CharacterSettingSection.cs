// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Configuration.Settings.Sections
{
    public class CharacterSettingSection : SettingSection
    {
        public override string Label => "Character";

        public override IconUsage Icon => FontAwesome.Solid.Walking;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new ButtonText
                {
                    Text = "Open Characters Folder",
                    Width = 200,
                },
            };
        }
    }
}
