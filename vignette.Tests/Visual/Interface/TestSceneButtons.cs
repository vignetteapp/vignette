using vignette.Graphics.Interface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;

namespace vignette.Tests.Visual.Interface
{
    public class TestSceneButtons : TestScene
    {
        public TestSceneButtons()
        {
            Add(new FillFlowContainer
            {
                AutoSizeAxes = Axes.X,
                RelativeSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new VignetteButton
                    {
                        Text = "disabled button",
                        Width = 300,
                    },
                    new VignetteButton
                    {
                        Text = "enabled button",
                        Width = 300,
                        Action = () => {}
                    },
                    new VignetteButton
                    {
                        Text = "custom button",
                        Width = 300,
                        BackgroundColor = Colour4.DarkOliveGreen,
                        Action = () => {}
                    },
                    new VignetteCheckbox
                    {
                        Text = "checkbox"
                    },
                    new FramedButton
                    {
                        Size = new Vector2(100),
                    },
                    new FramedButton
                    {
                        Size = new Vector2(100),
                        FrameColor = Colour4.Turquoise,
                        Action = () => {},
                        ShowBadge = true,
                    }
                }
            });
        }
    }
}