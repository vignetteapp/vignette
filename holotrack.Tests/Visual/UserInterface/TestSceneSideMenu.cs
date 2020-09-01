using holotrack.Screens.Main;
using osu.Framework.Graphics;
using osu.Framework.Testing;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneSideMenu : TestScene
    {
        public TestSceneSideMenu()
        {
            Add(new SideMenu
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}