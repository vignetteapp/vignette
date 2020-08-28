using holotrack.Core.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneButtons : TestSceneUserInterface
    {
        public TestSceneButtons()
        {
            Elements.AddRange(new Drawable[]
            {
                new HoloTrackButton
                {
                    Text = @"basic button",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Action = () => {},
                },
                new HoloTrackButton
                {
                    Text = @"disabled basic button",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new KeybindButton
                {
                    Text = @"keybind button",
                    ButtonText = @"Alt + F4",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Action = () => {},
                },
                new KeybindButton
                {
                    Text = @"disabled keybind button",
                    ButtonText = @"Alt + F4",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new FramedButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Action = () => {},
                },
                new FramedButton
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new FramedButton
                {
                    Icon = FontAwesome.Solid.Question,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Action = () => {},
                },
                new FramedButton
                {
                    Icon = FontAwesome.Solid.Question,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new FramedButton
                {
                    Icon = FontAwesome.Solid.Question,
                    FrameColor = Colour4.Red,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Action = () => {},
                }
            });
        }
    }
}