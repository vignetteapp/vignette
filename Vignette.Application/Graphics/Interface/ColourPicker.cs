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

        private readonly Bindable<Vector4> source = new Bindable<Vector4>();

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
                    Height = 50,
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
                                        RelativeSizeAxes = Axes.X,
                                    },
                                    hexText = new HexTextBox
                                    {
                                        RelativeSizeAxes = Axes.X,
                                    },
                                }
                            }
                        },
                    },
                },
            };

            hsvPicker.Current.BindTo(source);
            huePicker.Current.BindTo(source);

            hsvPicker.OnCommit += handleCommit;
            huePicker.OnCommit += handleCommit;

            Current.BindValueChanged(e => hexText.Current.Value = e.NewValue.ToHex(), true);
            source.BindValueChanged(e => preview.Colour = Colour4.FromHSV(e.NewValue.X, e.NewValue.Y, 1 - e.NewValue.Z, 1), true);

            hexText.OnCommit += handleTextCommit;
        }

        private void handleCommit()
        {
            var hsv = source.Value;
            Current.Value = Colour4.FromHSV(hsv.X, hsv.Y, 1 - hsv.Z, 1);
        }

        private void handleTextCommit(TextBox sender, bool textChanged)
        {
            if (!textChanged)
                return;

            var hsv = Colour4.FromHex(hexText.Current.Value).ToHSV();
            source.Value = new Vector4(hsv.X, hsv.Y, 1 - hsv.Z, 1);

            handleCommit();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            var hsv = Current.Value.ToHSV();
            source.Value = new Vector4(hsv.X, hsv.Y, 1 - hsv.Z, 1);
        }

        private class HSVPicker : Container, IHasCurrentValue<Vector4>
        {
            private readonly Box saturation;

            private readonly Nub nub;

            private readonly BindableWithCurrent<Vector4> current = new BindableWithCurrent<Vector4>();

            public event Action OnCommit;

            public Bindable<Vector4> Current
            {
                get => current.Current;
                set => current.Current = value;
            }

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

                Current.BindValueChanged(e =>
                {
                    float x = MathHelper.Clamp(e.NewValue.Y * DrawWidth, 0, DrawWidth);
                    float y = MathHelper.Clamp(e.NewValue.Z * DrawHeight, 0, DrawHeight);
                    nub.MoveTo(new Vector2(x, y), 200, Easing.OutQuint);

                    var colour = Colour4.FromHSV(Current.Value.X, 1, 1, 1);
                    saturation.Colour = ColourInfo.GradientHorizontal(colour.Opacity(0), colour);
                }, true);
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

            protected override void OnMouseUp(MouseUpEvent e)
            {
                base.OnMouseUp(e);
                OnCommit?.Invoke();
            }

            private void handleMouseEvent(MouseEvent e)
            {
                float xPercent = MathHelper.Clamp(e.MousePosition.X / DrawWidth, 0, 1);
                float yPercent = MathHelper.Clamp(e.MousePosition.Y / DrawHeight, 0, 1);
                Current.Value = new Vector4(Current.Value.X, xPercent, yPercent, 1);
            }
        }

        private class HuePicker : SliderBar<float>, IHasCurrentValue<Vector4>
        {
            private readonly Nub nub;

            public event Action OnCommit;

            public new Bindable<Vector4> Current
            {
                get => ((IHasCurrentValue<Vector4>)this).Current;
                set => ((IHasCurrentValue<Vector4>)this).Current = value;
            }

            public HuePicker()
            {
                Height = nub_size;
                RangePadding = nub_size / 2;
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

                base.Current = new BindableFloat
                {
                    Default = 0,
                    MinValue = 0,
                    MaxValue = 1,
                };

                base.Current.BindValueChanged(e => Current.Value = new Vector4(1 - e.NewValue, Current.Value.Y, Current.Value.Z, 1), true);
                Current.BindValueChanged(e => UpdateValue(1 - e.NewValue.X), true);
            }

            private readonly BindableWithCurrent<Vector4> current = new BindableWithCurrent<Vector4>();

            Bindable<Vector4> IHasCurrentValue<Vector4>.Current
            {
                get => current.Current;
                set => current.Current = value;
            }

            protected override void UpdateValue(float value)
            {
                nub.MoveToX(value, 200, Easing.OutQuint);
            }

            protected override void OnMouseUp(MouseUpEvent e)
            {
                base.OnMouseUp(e);
                OnCommit?.Invoke();
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                Current.TriggerChange();
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
                CommitOnFocusLost = true;
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
