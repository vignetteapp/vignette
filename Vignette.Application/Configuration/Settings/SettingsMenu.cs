// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using Vignette.Application.Configuration.Settings.Sections;
using Vignette.Application.Input;

namespace Vignette.Application.Configuration.Settings
{
    public class SettingsMenu : Container, IKeyBindingHandler<ApplicationAction>
    {
        private readonly SettingPanel panel;

        private readonly SettingNavigation navigation;

        public virtual IEnumerable<SettingSection> Sections => new SettingSection[]
        {
            new CharacterSettingSection(),
            new BackgroundSettingSection(),
            new ApplicationSettingSection(),
            new PlatformSettingSection(),
        };

        public SettingsMenu()
        {
            AddRange(new Drawable[]
            {
                panel = new SettingPanel(Sections)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                },
                navigation = new SettingNavigation(Sections),
            });

            navigation.OnHome += handleHomeAction;
            navigation.OnSection += handleSectionAction;
        }

        private void handleHomeAction()
        {
            navigation.Hide();
            panel.Hide();
        }

        private void handleSectionAction(SettingSection section)
        {
            if (panel.State.Value == Visibility.Hidden)
                panel.Show();

            panel.ScrollToSection(section);
        }

        public bool OnPressed(ApplicationAction action)
        {
            switch (action)
            {
                case ApplicationAction.ToggleNavigation:
                    navigation.ToggleVisibility();
                    panel.Hide();
                    break;
            }

            return true;
        }

        public void OnReleased(ApplicationAction action)
        {
        }
    }
}
