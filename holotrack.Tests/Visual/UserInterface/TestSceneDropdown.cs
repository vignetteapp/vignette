using holotrack.Graphics.UserInterface;
using osu.Framework.Graphics;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneDropdown : TestSceneUserInterface
    {
        public TestSceneDropdown()
        {
            Elements.Add(new HoloTrackDropdown<string>
            {
                RelativeSizeAxes = Axes.X,
                Items = new[] { "Hello", "World", "Testing" }
            });
        }   
    }
}