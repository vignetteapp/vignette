// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osuTK;

namespace Vignette.Application.Graphics
{
    public static class VignetteStyle
    {
        public static float CornerRadius => 5.0f;

        public static float BorderThickness => 2.5f;

        public static EdgeEffectParameters ElevationOne => new EdgeEffectParameters
        {
            Colour = Colour4.Black.Opacity(0.4f),
            Offset = Vector2.Zero,
            Type = EdgeEffectType.Shadow,
            Radius = 4.0f
        };

        public static EdgeEffectParameters ElevationTwo => new EdgeEffectParameters
        {
            Colour = Colour4.Black.Opacity(0.3f),
            Offset = Vector2.Zero,
            Type = EdgeEffectType.Shadow,
            Radius = 8.0f
        };

        public static EdgeEffectParameters ElevationThree => new EdgeEffectParameters
        {
            Colour = Colour4.Black.Opacity(0.2f),
            Offset = Vector2.Zero,
            Type = EdgeEffectType.Shadow,
            Radius = 12.0f
        };
    }
}
