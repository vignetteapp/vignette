using System.Linq;
using System.Reflection;
using holotrack.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneFonts : TestScene
    {
        public TestSceneFonts()
        {
            TextFlowContainer flow;
            Add(flow = new TextFlowContainer());

            flow.AddParagraph("Default", s => s.Font = HoloTrackFont.Default);
            flow.AddParagraph("DefaultItalic", s => s.Font = HoloTrackFont.Default.With(italics: true));

            flow.AddParagraph("Light", s => s.Font = HoloTrackFont.Light);
            flow.AddParagraph("LightItalic", s => s.Font = HoloTrackFont.Light.With(italics: true));

            flow.AddParagraph("Bold", s => s.Font = HoloTrackFont.Bold);
            flow.AddParagraph("BoldItalic", s => s.Font = HoloTrackFont.Bold.With(italics: true));

            flow.AddParagraph("Medium", s => s.Font = HoloTrackFont.Medium);
            flow.AddParagraph("MediumItalic", s => s.Font = HoloTrackFont.Medium.With(italics: true));

            flow.AddParagraph("Black", s => s.Font = HoloTrackFont.Black);
            flow.AddParagraph("BlackItalic", s => s.Font = HoloTrackFont.Black.With(italics: true));
        }
    }
}