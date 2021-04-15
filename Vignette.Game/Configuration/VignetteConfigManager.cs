// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Bindables;
using Vignette.Game.Graphics.Backgrounds;

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
            SetDefault(VignetteSetting.ShowFpsOverlay, false);
            SetDefault(VignetteSetting.CameraDevice, string.Empty);
            SetDefault(VignetteSetting.BackgroundType, BackgroundType.Image);
            SetDefault(VignetteSetting.BackgroundAsset, string.Empty);
            SetDefault(VignetteSetting.BackgroundColour, Colour4.Green);
            SetDefault(VignetteSetting.BackgroundOffset, Vector2.Zero);
            SetDefault(VignetteSetting.BackgroundScale, 1.0f, 0.1f, 10.0f, 0.1f);
            SetDefault(VignetteSetting.BackgroundRotation, 0.0f, 0.0f, 360.0f, 0.1f);
            SetDefault(VignetteSetting.BackgroundColourBlending, BackgroundColourBlending.Inherit);
            SetDefault(VignetteSetting.Theme, "Default");
        }

        protected override void AddBindable<TBindable>(VignetteSetting lookup, Bindable<TBindable> bindable)
        {
            switch (lookup)
            {
                case VignetteSetting.BackgroundColour:
                    base.AddBindable(lookup, new BindableColour4(Colour4.Green));
                    break;

                case VignetteSetting.BackgroundOffset:
                    base.AddBindable(lookup, new BindableVector2());
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

        ShowFpsOverlay,

        CameraDevice,

        BackgroundType,

        BackgroundAsset,

        BackgroundColour,

        BackgroundOffset,

        BackgroundScale,

        BackgroundRotation,

        BackgroundColourBlending,

        Theme,
    }
}
