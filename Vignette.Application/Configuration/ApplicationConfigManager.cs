// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osuTK;

namespace Vignette.Application.Configuration
{
    public class ApplicationConfigManager : IniConfigManager<ApplicationConfig>
    {
        protected override string Filename => @"app.ini";

        public ApplicationConfigManager(Storage storage)
            : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(ApplicationConfig.Theme, @"Default");
            Set(ApplicationConfig.BackgroundColour, Colour4.Green);
            Set(ApplicationConfig.BackgroundImage, string.Empty);
            Set(ApplicationConfig.BackgroundVideo, string.Empty);
            Set(ApplicationConfig.BackgroundType, BackgroundTypes.Colour);
            Set(ApplicationConfig.BackgroundOffset, Vector2.Zero);
            Set(ApplicationConfig.BackgroundScale, Vector2.One);
        }
    }

    public enum ApplicationConfig
    {
        Theme,

        BackgroundColour,

        BackgroundImage,

        BackgroundVideo,

        BackgroundType,

        BackgroundOffset,

        BackgroundScale,
    }
}
