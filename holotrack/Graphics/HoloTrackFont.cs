using osu.Framework.Graphics.Sprites;

namespace holotrack.Graphics
{
    public static class HoloTrackFont
    {
        public static FontUsage Default => new FontUsage("NotoExtraCond");
        public static FontUsage Bold => Default.With(weight: "Bold");
        public static FontUsage Black => Default.With(weight: "Black");
        public static FontUsage Light => Default.With(weight: "Light");
        public static FontUsage Medium => Default.With(weight: "Medium");
    }
}