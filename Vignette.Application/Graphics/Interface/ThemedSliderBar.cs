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
    public class ThemedSliderBar<T> : SliderBar<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private const float slider_bar_height = 18.0f;

        private const float slider_bar_thickness = 2.0f;

        private readonly ThemedSolidBox left;

        private readonly ThemedSolidBox right;

        private readonly ThemedSolidCircle nub;

        public ThemedSliderBar()
        {
            Height = slider_bar_height;
            RangePadding = slider_bar_height / 2;

            AddRange(new Drawable[]
            {
                left = new ThemedSolidBox
                {
                    Width = 0.5f,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Height = slider_bar_thickness,
                    Position = new Vector2(2, 0),
                    ThemeColour = ThemeColour.ThemePrimary,
                    RelativeSizeAxes = Axes.None,
                },
                right = new ThemedSolidBox
                {
                    Width = 0.5f,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Height = slider_bar_thickness,
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
                        Child = nub = new ThemedSolidCircle
                        {
                            RelativePositionAxes = Axes.X,
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
