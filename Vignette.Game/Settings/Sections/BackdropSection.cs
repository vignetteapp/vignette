// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Screens.Stage;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class BackdropSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.Image;

        public override LocalisableString Label => "Backdrop";

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, SessionConfigManager session)
        {
            Children = new Drawable[]
            {
                new SettingsColourPicker
                {
                    Icon = SegoeFluent.ColorBackground,
                    Label = "Background Colour",
                    Current = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour),
                },
            };
        }
    }
}
