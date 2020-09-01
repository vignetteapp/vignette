using holotrack.Graphics.UserInterface;
using osu.Framework.Graphics;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneInputBox : TestSceneUserInterface
    {
        public TestSceneInputBox()
        {
            Elements.AddRange(new[]
            {
                new HoloTrackTextBox
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                new HoloTrackTextBox
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    PlaceholderText = @"has placeholder"
                },
                new HoloTrackTextBox
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