// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Screens.Stage;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class SceneSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.Image;

        public override LocalisableString Label => "Scene";

        public SceneSection()
        {
            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Label = "Background",
                    Children = new Drawable[]
                    {
                        new SettingsEnumDropdown<BackgroundType>
                        {
                            Label = "Background",
                            Icon = SegoeFluent.Wallpaper,
                        },
                        new SettingsColourPicker
                        {
                            Label = "Color"
                        },
                    },
                },
            };
        }
    }
}
