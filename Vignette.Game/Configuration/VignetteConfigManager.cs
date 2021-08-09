// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Drawing;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using Vignette.Game.Bindables;

namespace Vignette.Game.Configuration
{
    public class VignetteConfigManager : IniConfigManager<VignetteSetting>
    {
        protected override string Filename => "config.ini";

        public VignetteConfigManager(Storage storage, IDictionary<VignetteSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetDefault(VignetteSetting.WindowResizable, false);
            SetDefault(VignetteSetting.WindowSize, new Size(1366, 768));
            SetDefault(VignetteSetting.ShowFpsOverlay, false);
            SetDefault(VignetteSetting.CameraDevice, string.Empty);
            SetDefault(VignetteSetting.BackgroundColour, Colour4.Green);
            SetDefault(VignetteSetting.Theme, "Light");
        }

        protected override void AddBindable<TBindable>(VignetteSetting lookup, Bindable<TBindable> bindable)
        {
            switch (lookup)
            {
                case VignetteSetting.BackgroundColour:
                    base.AddBindable(lookup, new BindableColour4(Colour4.Green));
                    break;

                case VignetteSetting.WindowSize:
                    base.AddBindable(lookup, new BindableSize(new Size(1366, 768)));
                    break;

                default:
                    base.AddBindable(lookup, bindable);
                    break;
            }
        }
    }

    public enum VignetteSetting
    {
        WindowResizable,
        WindowSize,
        ShowFpsOverlay,
        CameraDevice,
        BackgroundColour,
        Theme,
    }
}
