// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
    public class KeyboardSection : SettingsSection
    {
        public override LocalisableString Label => "Keyboard";

        public override IconUsage Icon => SegoeFluent.Keyboard;

        [BackgroundDependencyLoader]
        private void load(VignetteKeyBindManager keybindManager)
        {
            Children = new Drawable[]
            {
                new SettingsSubSection
                {
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

        private class KeybindConfigurationPanel : SettingsSubPanel
        {
        }
    }
}
