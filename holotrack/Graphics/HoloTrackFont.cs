using osu.Framework.Graphics.Sprites;

namespace holotrack.Graphics
{
    public static class HoloTrackFont
    {
        public static FontUsage Default => new FontUsage("NotoExtraCond");
        public static FontUsage Italic => Default.With(italics: true);
        public static FontUsage Bold => Default.With(weight: "Bold");
        public static FontUsage BoldItalic => Bold.With(italics: true);
        public static FontUsage Black => Default.With(weight: "Black");
        public static FontUsage BlackItalic => Black.With(italics: true);
        public static FontUsage Light => Default.With(weight: "Light");
        public static FontUsage LightItalic => Light.With(italics: true);
        public static FontUsage Medium => Default.With(weight: "Medium");
        public static FontUsage MediumItalic => Medium.With(italics: true);
    }
}