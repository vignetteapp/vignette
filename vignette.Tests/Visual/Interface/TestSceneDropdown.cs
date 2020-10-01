using vignette.Graphics.Interface;
using osu.Framework.Testing;

namespace vignette.Tests.Visual.Interface
{
    public class TestSceneDropdown : TestScene
    {
        public TestSceneDropdown()
        {
            Add(new VignetteDropdown<string>
            {
                Items = new [] { "Hello", "World" },
                Width = 300,
            });
        }
    }
}