using holotrack.Core.Graphics.UserInterface.Control;
using osu.Framework.Bindables;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneSliderBar : TestSceneUserInterface
    {
        public TestSceneSliderBar()
        {
            Elements.Add(new BasicSliderBar<float>
            {
                Current = new BindableFloat
                {
                    MinValue = 0,
                    MaxValue = 1,
                    Default = 0,
                    Value = 0,
                }
            });
        }
    }
}