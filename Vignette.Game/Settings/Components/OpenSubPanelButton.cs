// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;
using System;

namespace Vignette.Game.Settings.Components
{
    public class OpenSubPanelButton<T> : SettingsInteractableItem
        where T : SettingsSubPanel
    {
        private readonly object[] args;

        public OpenSubPanelButton(params object[] args)
        {
            this.args = args;
        }

        [BackgroundDependencyLoader]
        private void load(SettingsOverlay overlay)
        {
            Foreground.Add(new ThemableSpriteIcon
            {
                Size = new Vector2(12),
                Icon = SegoeFluent.ChevronRight,
                Colour = ThemeSlot.Black,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                Margin = new MarginPadding { Right = 6 },
            });

            Action = () => overlay?.ShowSubPanel(Activator.CreateInstance(typeof(T), args) as T);
        }
    }
}
