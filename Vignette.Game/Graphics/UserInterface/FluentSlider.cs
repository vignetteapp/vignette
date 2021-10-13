// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A slider bar where its value can be adjusted by dragging the nub.
    /// </summary>
    public class FluentSlider<T> : SliderBar<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private readonly ThemableBox unfilled;
        private readonly ThemableBox filled;
        private readonly ThemableCircle nub;

        public FluentSlider()
        {
            Height = 28;
            Children = new Drawable[]
            {
                new Container
                {
                    Height = 4,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    CornerRadius = 2.5f,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        unfilled = new ThemableBox
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                        filled = new ThemableBox
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Right = 16 },
                    Child = nub = new ThemableCircle
                    {
                        Size = new Vector2(16),
                        BorderThickness = 2.5f,
                        Colour = ThemeSlot.Gray30,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        RelativePositionAxes = Axes.X,
                    },
                },
            };

            Current.BindDisabledChanged(_ => updateState(), true);
        }

        private void updateState()
        {
            if (!Current.Disabled)
            {
                if (IsHovered || isPressed)
                {
                    filled.Colour = ThemeSlot.AccentPrimary;
                    unfilled.Colour = ThemeSlot.AccentLighter;
                    nub.BorderColour = ThemeSlot.AccentDark;
                }
                else
                {
                    filled.Colour = ThemeSlot.Gray130;
                    unfilled.Colour = ThemeSlot.Gray60;
                    nub.BorderColour = ThemeSlot.Gray130;
                }
            }
            else
            {
                filled.Colour = ThemeSlot.Gray90;
                unfilled.Colour = ThemeSlot.Gray20;
                nub.BorderColour = ThemeSlot.Gray60;
            }
        }

        private bool hasInitialWidth;

        protected override void UpdateValue(float value)
        {
            nub.MoveToX(value, hasInitialWidth ? 200 : 0, Easing.OutQuint);
            filled.ResizeWidthTo(value, hasInitialWidth ? 200 : 0, Easing.OutQuint);
            hasInitialWidth = true;
        }

        private bool isPressed;

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (Current.Disabled)
                return false;

            isPressed = true;
            updateState();

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (Current.Disabled)
                return;

            isPressed = false;
            updateState();

            base.OnMouseUp(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (Current.Disabled)
                return false;

            updateState();

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (Current.Disabled)
                return;

            updateState();

            base.OnHoverLost(e);
        }
    }
}
