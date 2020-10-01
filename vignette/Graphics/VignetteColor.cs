using osu.Framework.Graphics;

namespace vignette.Graphics
{
    public static class VignetteColor
    {
        public static Colour4 Base => Colour4.FromHex("362f2d");
        public static Colour4 Light => Base.Lighten(0.3f);
        public static Colour4 Lighter => Base.Lighten(0.6f);
        public static Colour4 Lightest => Base.Lighten(0.9f);
        public static Colour4 Dark => Base.Darken(0.3f);
        public static Colour4 Darker => Base.Darken(0.6f);
        public static Colour4 Darkest => Base.Darken(0.9f);
    }
}