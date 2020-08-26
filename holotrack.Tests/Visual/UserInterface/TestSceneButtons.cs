using holotrack.Core.Graphics.UserInterface.Control;
using osu.Framework.Graphics;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneButtons : TestSceneUserInterface
    {
        public TestSceneButtons()
        {
            Elements.AddRange(new Drawable[]
            {
                new BasicButton
                {
                    Text = @"basic button",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new KeybindButton
                {
                    Text = @"keybind button",
                    ButtonText = @"Alt + F4",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new SpriteButton
                {
                    Text = @"basic sprite button",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new BadgedSpriteButton
                {
                    Text = @"badged sprite button",
                    BadgeText = @"?",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });
        }
    }
}