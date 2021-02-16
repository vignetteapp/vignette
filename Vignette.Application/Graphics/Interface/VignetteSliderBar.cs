// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class VignetteSliderBar<T> : SliderBar<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private const float slider_bar_height = 25.0f;

        private readonly VignetteBox left;

        private readonly VignetteBox right;

        private readonly OutlinedBox nub;

        public VignetteSliderBar()
        {
            Height = slider_bar_height;
            RangePadding = slider_bar_height / 2;

            AddRange(new Drawable[]
            {
                left = new VignetteBox
                {
                    Width = 0.5f,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Height = 6.0f,
                    Position = new Vector2(2, 0),
                    ThemeColour = ThemeColour.ThemePrimary,
                    RelativeSizeAxes = Axes.None,
                },
                right = new VignetteBox
                {
                    Width = 0.5f,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Height = 6.0f,
                    Position = new Vector2(-2, 0),
                    ThemeColour = ThemeColour.NeutralQuaternaryAlt,
                    RelativeSizeAxes = Axes.None,
                },
                new Container
                {
                    Padding = new MarginPadding { Horizontal = RangePadding },
                    RelativeSizeAxes = Axes.Both,
                    Child = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = nub = new OutlinedBox
                        {
                            RelativePositionAxes = Axes.X,
                            BorderThickness = 6.0f,
                            CornerRadius = RangePadding,
                            ThemeColour = ThemeColour.ThemePrimary,
                            Origin = Anchor.TopCentre,
                            Size = new Vector2(slider_bar_height),
                        },
                    },
                },
            });
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();
            left.Width = Math.Clamp(RangePadding + nub.DrawPosition.X - nub.DrawWidth / 2, 0, DrawWidth);
            right.Width = Math.Clamp(DrawWidth - nub.DrawPosition.X - RangePadding - nub.DrawWidth / 2, 0, DrawWidth);
        }

        protected override void UpdateValue(float value)
        {
            nub.MoveToX(value, 200, Easing.OutQuint);
        }
    }
}
