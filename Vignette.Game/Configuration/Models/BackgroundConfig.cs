// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osuTK;

namespace Vignette.Game.Configuration.Models
{
    [Serializable]
    public class BackgroundConfig
    {
        public Bindable<BackgroundType> Type = new Bindable<BackgroundType>();

        public Bindable<string> Asset = new Bindable<string>();

        public Bindable<Colour4> Colour = new Bindable<Colour4>();

        public Bindable<Vector2> Offset = new Bindable<Vector2>();
    }

    public enum BackgroundType
    {
        Colour,

        Image,

        Video,
    }
}
