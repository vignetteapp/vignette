using vignette.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;

namespace vignette.Tests.Visual.Interface
{
    public class TestSceneColors : TestScene
    {
        public TestSceneColors()
        {
            Add(new FillFlowContainer<Box>
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Box[]
                {
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Base
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Light
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Lighter
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Lightest
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Dark
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Darker
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = VignetteColor.Darkest
                    },
                }
            });
        }
    }
}