using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.UserInterface
{
    public abstract class TestSceneUserInterface : TestScene
    {
        protected readonly FillFlowContainer Elements;

        public TestSceneUserInterface()
        {
            Add(Elements = new FillFlowContainer
            {
                Width = 200,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Spacing = new Vector2(0, 6),
                AutoSizeAxes = Axes.Y
            });
        }
    }
}