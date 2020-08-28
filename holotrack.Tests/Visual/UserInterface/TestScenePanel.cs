using holotrack.Core.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestScenePanel : TestScene
    {
        public TestScenePanel()
        {
            Add(new Box
            {
                Size = new Vector2(512),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Colour4.Cyan,
            });

            Add(new HoloTrackPanel
            {
                Size = new Vector2(256),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}