using holotrack.Core.Graphics.UserInterface.Control;
using osu.Framework.Graphics;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneInputBox : TestSceneUserInterface
    {
        public TestSceneInputBox()
        {
            Elements.AddRange(new[]
            {
                new BasicInputBox
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new BasicInputBox
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    PlaceholderText = @"has placeholder"
                },
                new BasicInputBox
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = @"has content"
                },
            });
        }
    }
}