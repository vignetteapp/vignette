using holotrack.Graphics.UserInterface;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneCheckbox : TestSceneUserInterface
    {
        public TestSceneCheckbox()
        {
            Elements.Add(new HoloTrackCheckbox {Text = @"basic checkbox" });
        }
    }
}