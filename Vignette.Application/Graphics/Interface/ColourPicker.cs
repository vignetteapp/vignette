// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class ColourPicker : FillFlowContainer, IHasCurrentValue<Colour4>
    {
        private const float nub_size = 18.0f;

        private readonly HSVPicker hsvPicker;

        private readonly HuePicker huePicker;

        private readonly HexTextBox hexText;

        private readonly Box preview;

        private readonly BindableWithCurrent<Colour4> current = new BindableWithCurrent<Colour4>();

        public Bindable<Colour4> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        public ColourPicker()
        {
            Width = 200;
            Spacing = new Vector2(0, 10);
            Direction = FillDirection.Vertical;
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                hsvPicker = new HSVPicker
                {
                    Size = new Vector2(200),
                },
                new GridContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize),
                        new Dimension()
                    },
                    Content = new Drawable[][]
                    {
                        new Drawable[]
                        {
                            new Container
                            {
                                Size = new Vector2(50),
                                Masking = true,
                                CornerRadius = 5.0f,
                                Child = preview = new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                },
                            },
                            new FillFlowContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Direction = FillDirection.Vertical,
                                Spacing = new Vector2(0, 6),
                                Padding = new MarginPadding { Left = 10 },
                                Children = new Drawable[]
                                {
                                    huePicker = new HuePicker
                                    {
                                        Current = new BindableFloat
                                        {
                                            MinValue = 0,
                                            MaxValue = 1,
                                        },
                                    },
                                    hexText = new HexTextBox
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        Current = new Bindable<string>(),
                                    },
                                }
                            }
                        },
                    },
                },
            };

            hsvPicker.Hue.BindTo(huePicker.Current);
            hsvPicker.Hue.ValueChanged += _ => handleHSVChange();
            hsvPicker.Saturation.ValueChanged += _ => handleHSVChange();
            hsvPicker.Value.ValueChanged += _ => handleHSVChange();

            handleHSVChange();
        }

        private void handleHSVChange()
        {
            Current.Value = Colour4.FromHSV(1 - hsvPicker.Hue.Value, hsvPicker.Saturation.Value, 1 - hsvPicker.Value.Value);
            hexText.Current.Value = Current.Value.ToHex();
            preview.Colour = Current.Value;
        }

        private class HSVPicker : Container
        {
            private readonly Box saturation;

            private readonly Nub nub;

            public Bindable<float> Hue { get; } = new Bindable<float>();

            public Bindable<float> Saturation { get; } = new Bindable<float>();

            public Bindable<float> Value { get; } = new Bindable<float>();

            public HSVPicker()
            {
                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        CornerRadius = 5.0f,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            },
                            saturation = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            },
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = ColourInfo.GradientVertical(Colour4.Black.Opacity(0), Colour4.Black),
                            },
                        },
                    },
                    nub = new Nub { Origin = Anchor.Centre },
                };
                
                Hue.BindValueChanged(e =>
                {
                    var colour = Colour4.FromHSV(1 - e.NewValue, 1, 1);
                    saturation.Colour = ColourInfo.GradientHorizontal(colour.Opacity(0), colour);
                }, true);

                Saturation.BindValueChanged(e => nub.MoveToX(e.NewValue * DrawWidth, 200, Easing.OutQuint), true);
                Value.BindValueChanged(e => nub.MoveToY(e.NewValue * DrawHeight, 200, Easing.OutQuint), true);
            }

            protected override bool OnMouseDown(MouseDownEvent e)
            {
                handleMouseEvent(e);
                return base.OnMouseDown(e);
            }

            protected override bool OnDragStart(DragStartEvent e) => true;

            protected override void OnDrag(DragEvent e)
            {
                base.OnDrag(e);
                handleMouseEvent(e);
            }

            private void handleMouseEvent(MouseButtonEvent e)
            {
                Saturation.Value = Math.Clamp(e.MousePosition.X / DrawWidth, 0, 1);
                Value.Value = Math.Clamp(e.MousePosition.Y / DrawHeight, 0, 1);
            }
        }

        private class HuePicker : SliderBar<float>
        {
            private readonly Nub nub;

            public HuePicker()
            {
                Height = nub_size;
                RangePadding = nub_size / 2;
                RelativeSizeAxes = Axes.X;
                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        CornerRadius = nub_size / 2,
                        Masking = true,
                        Child = new GridContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Content = new Drawable[][]
                            {
                                new[]
                                {
                                    new HorizontalGradientBox(Colour4.Red, Colour4.Magenta),
                                    new HorizontalGradientBox(Colour4.Magenta, Colour4.Blue),
                                    new HorizontalGradientBox(Colour4.Blue, Colour4.Aqua),
                                    new HorizontalGradientBox(Colour4.Aqua, Colour4.Lime),
                                    new HorizontalGradientBox(Colour4.Lime, Colour4.Yellow),
                                    new HorizontalGradientBox(Colour4.Yellow, Colour4.Red),
                                },
                            }
                        },
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding { Horizontal = nub_size / 2 },
                        Child = nub = new Nub
                        {
                            RelativePositionAxes = Axes.X,
                            Origin = Anchor.TopCentre,
                        },
                    },
                };
            }

            protected override void UpdateValue(float value)
            {
                nub.MoveToX(value, 200, Easing.OutQuint);
            }

            private class HorizontalGradientBox : Box
            {
                public HorizontalGradientBox(Colour4 start, Colour4 end)
                {
                    RelativeSizeAxes = Axes.Both;
                    Colour = ColourInfo.GradientHorizontal(start, end);
                }
            }
        }

        private class Nub : CircularContainer
        {
            public Nub()
            {
                Size = new Vector2(nub_size);
                Child = new Box { RelativeSizeAxes = Axes.Both };
                Masking = true;
                BorderColour = Colour4.Black;
                BorderThickness = 1.5f;
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Colour = Colour4.Black.Opacity(0.5f),
                    Radius = 5.0f,
                    Hollow = true,
                };
            }
        }

        private class HexTextBox : ThemedTextBox
        {
            public HexTextBox()
            {
                LengthLimit = 6;
                TextContainer.Padding = new MarginPadding { Left = 20 };
                Add(new ThemedSpriteIcon
                {
                    Icon = FluentSystemIcons.Filled.NumberSymbol24,
                    Size = new Vector2(12),
                    Depth = 1,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Left = 8 },
                    ThemeColour = ThemeColour.NeutralPrimary,
                });
            }

            protected override bool CanAddCharacter(char character) => Uri.IsHexDigit(character);
        }
    }
}
