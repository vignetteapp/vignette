// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Input;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class InputSection : SettingsSection
    {
        public override LocalisableString Label => "Input";

        public override IconUsage Icon => SegoeFluent.Keyboard;

        [BackgroundDependencyLoader]
        private void load(VignetteKeyBindManager keybindManager)
        {
            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Label = "Keyboard",
                    Children = new Drawable[]
                    {
                        new SettingsSwitch
                        {
                            Label = "Allow keyboard to open settings"
                        },
                        new OpenSubPanelButton<KeybindConfigurationPanel>
                        {
                            Label = "Configure Keybindings",
                        },
                        new OpenExternalLinkButton(new FileInfo(keybindManager.FilePath))
                        {
                            Label = "Open keybind config",
                        },
                    }
                },
            };
        }

        private class KeybindConfigurationPanel : SettingsPanel
        {
        }
    }
}
