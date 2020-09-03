using holotrack.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.Interface
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
                        Colour = HoloTrackColor.Base
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = HoloTrackColor.Light
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = HoloTrackColor.Lighter
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = HoloTrackColor.Lightest
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = HoloTrackColor.Dark
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = HoloTrackColor.Darker
                    },
                    new Box
                    {
                        Size = new Vector2(25),
                        Colour = HoloTrackColor.Darkest
                    },
                }
            });
        }
    }
}