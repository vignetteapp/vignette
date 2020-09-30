using System.Linq;
using System.Reflection;
using vignette.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;

namespace vignette.Tests.Visual.Interface
{
    public class TestSceneFonts : TestScene
    {
        public TestSceneFonts()
        {
            TextFlowContainer flow;
            Add(flow = new TextFlowContainer());

            flow.AddParagraph("Default", s => s.Font = VignetteFont.Default);
            flow.AddParagraph("DefaultItalic", s => s.Font = VignetteFont.Default.With(italics: true));

            flow.AddParagraph("Light", s => s.Font = VignetteFont.Light);
            flow.AddParagraph("LightItalic", s => s.Font = VignetteFont.Light.With(italics: true));

            flow.AddParagraph("Bold", s => s.Font = VignetteFont.Bold);
            flow.AddParagraph("BoldItalic", s => s.Font = VignetteFont.Bold.With(italics: true));

            flow.AddParagraph("Medium", s => s.Font = VignetteFont.Medium);
            flow.AddParagraph("MediumItalic", s => s.Font = VignetteFont.Medium.With(italics: true));

            flow.AddParagraph("Black", s => s.Font = VignetteFont.Black);
            flow.AddParagraph("BlackItalic", s => s.Font = VignetteFont.Black.With(italics: true));
        }
    }
}