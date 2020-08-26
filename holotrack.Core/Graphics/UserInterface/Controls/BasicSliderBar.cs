using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class BasicSliderBar<T> : SliderBar<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private readonly Container nubContainer;
        private readonly Circle nub;

        public BasicSliderBar()
        {
            RangePadding = 9;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            AddRange(new Drawable[]
            {
                new Box
                {
                    Height = 3,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Colour = Colour4.FromHex("7d7d7d")
                },
                nubContainer = new Container
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Child = nub = new Circle
                    {
                        Size = new Vector2(18),
                        Origin = Anchor.TopCentre,
                        Colour = Colour4.FromHex("7d7d7d"),
                        RelativePositionAxes = Axes.X,
                    }
                }
            });
        }

        protected override void Update()
        {
            base.Update();

            nubContainer.Padding = new MarginPadding { Horizontal = RangePadding };
        }

        protected override void UpdateValue(float value)
        {
            nub.MoveToX(value);
        }
    }
}