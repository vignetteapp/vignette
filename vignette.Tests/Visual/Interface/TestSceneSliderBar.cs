using vignette.Graphics.Interface;
using osu.Framework.Bindables;
using osu.Framework.Testing;

namespace vignette.Tests.Visual.Interface
{
    public class TestSceneSliderBar : TestScene
    {
        public TestSceneSliderBar()
        {
            Add(new VignetteSliderBar<float>
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