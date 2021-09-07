// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
        private object[] args;

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

            Action = () => overlay.ShowSubPanel(Activator.CreateInstance(typeof(T), args) as T);
        }
    }
}
