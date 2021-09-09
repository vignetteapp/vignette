// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics
{
    public static class VignetteColour
    {
        public static Colour4 Primary => VignetteGameBase.IsInsidersBuild ? PrimaryInsider : PrimaryGeneral;

        public static Colour4 Secondary => VignetteGameBase.IsInsidersBuild ? SecondaryInsider : SecondaryGeneral;

        public static readonly Colour4 PrimaryGeneral = Colour4.FromHex("BE58CB");
        public static readonly Colour4 PrimaryInsider = Colour4.FromHex("58CB86");
        public static readonly Colour4 SecondaryGeneral = Colour4.FromHex("F10E5A");
        public static readonly Colour4 SecondaryInsider = Colour4.FromHex("0EBBF1");
    }
}
