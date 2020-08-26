using holotrack.Core.Graphics.UserInterface.Control;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneCheckbox : TestSceneUserInterface
    {
        public TestSceneCheckbox()
        {
            Elements.Add(new BasicCheckbox {Text = @"basic checkbox" });
        }
    }
}