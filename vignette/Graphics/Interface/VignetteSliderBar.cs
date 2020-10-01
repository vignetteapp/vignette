using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace vignette.Graphics.Interface
{
    public class VignetteSliderBar<T> : SliderBar<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private readonly Box leftBar;
        private readonly Box rightBar;
        private readonly Container nubContainer;
        private readonly Nub nub;

        public VignetteSliderBar()
        {
            Height = 25;
            RangePadding = 9;

            AddRange(new Drawable[]
            {
                leftBar = new Box
                {
                    Depth = 1,
                    Height = 4,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.None,
                    Colour = VignetteColor.Lighter,
                },
                rightBar = new Box
                {
                    Depth = 1,
                    Height = 4,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.None,
                    Colour = VignetteColor.Darkest,
                },
                nubContainer = new Container
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Child = nub = new Nub(),
                }
            });
        }

        protected override void Update()
        {
            base.Update();
            nubContainer.Padding = new MarginPadding { Horizontal = RangePadding };
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();
            leftBar.Scale = new Vector2(Math.Clamp(RangePadding + (nub.DrawPosition.X + 0.5f) - nub.DrawWidth / 2, 0, DrawWidth), 1);
            rightBar.Scale = new Vector2(Math.Clamp(DrawWidth - (nub.DrawPosition.X - 0.5f) - RangePadding - nub.DrawWidth / 2, 0, DrawWidth), 1);
        }

        protected override void UpdateValue(float value)
        {
            nub.MoveToX(value, 250, Easing.OutQuint);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            nub.ScaleTo(1.1f, 100);
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);
            nub.ScaleTo(1, 100);
        }

        private class Nub : CircularContainer
        {
            public Nub()
            {
                Size = new Vector2(20);
                Origin = Anchor.TopCentre;
                RelativePositionAxes = Axes.X;
                Masking = true;
                BorderColour = VignetteColor.Lighter;
                BorderThickness = 5;

                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent,
                };
            }
        }
    }
}