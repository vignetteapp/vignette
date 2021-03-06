// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Configuration.Settings.Sections
{
    public class BackgroundSettingSection : SettingSection
    {
        public override string Label => "Background";

        public override IconUsage Icon => FontAwesome.Solid.Images;

        [BackgroundDependencyLoader]
        private void load(Storage storage)
        {
            Children = new Drawable[]
            {
                new ButtonText
                {
                    Text = "Open Backgrounds Folder",
                    Width = 200,
                    Action = () => storage.OpenPathInNativeExplorer("./backgrounds"),
                },
            };
        }
    }
}
