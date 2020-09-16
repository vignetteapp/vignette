using holotrack.Graphics.Interface;
using osu.Framework.Testing;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneDropdown : TestScene
    {
        public TestSceneDropdown()
        {
            Add(new HoloTrackDropdown<string>
            {
                Items = new [] { "Hello", "World" },
                Width = 300,
            });
        }
    }
}