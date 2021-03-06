// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Platform;
using Vignette.Application.Configuration.Settings;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Input;

namespace Vignette.Application
{
    public class VignetteApplication : VignetteApplicationBase
    {
        public VignetteApplication()
        {
            Name = @"Vignette";
        }

        protected virtual SettingsMenu CreateSettingMenu() => new SettingsMenu();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Add(new ApplicationActionKeyBindingContainer
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Background
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    CreateSettingMenu().With(m => m.RelativeSizeAxes = Axes.Both),
                }
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.Title = Name;
        }
    }
}
