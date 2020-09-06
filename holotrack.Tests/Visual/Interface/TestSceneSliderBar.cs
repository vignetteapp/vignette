using holotrack.Graphics.Interface;
using osu.Framework.Bindables;
using osu.Framework.Testing;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneSliderBar : TestScene
    {
        public TestSceneSliderBar()
        {
            Add(new HoloTrackSliderBar<float>
            {
                Width = 300,
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