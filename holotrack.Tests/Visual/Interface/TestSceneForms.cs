using holotrack.Graphics.Interface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneForms : TestScene
    {
        public TestSceneForms()
        {
            Add(new HoloTrackForm
            {
                Size = new Vector2(400, 300),
                Title = "Test Form",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}