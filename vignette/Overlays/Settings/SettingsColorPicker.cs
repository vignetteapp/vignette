using System;
using vignette.Graphics;
using vignette.Graphics.Interface;
using vignette.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace vignette.Overlays.Settings
{
    public class SettingsColorPicker : SettingsItem<Colour4>
    {
        protected override Drawable CreateControl() => new ColorPicker
        {
            RelativeSizeAxes = Axes.X,
        };

        // We have to wrap the textbox with another container so we don't hide the textbox's IHasCurrentValue
        private class ColorPicker : Container, IHasCurrentValue<Colour4>
        {
            private readonly LabelledColorTextBox textBox;
            private readonly BindableWithCurrent<Colour4> current = new BindableWithCurrent<Colour4>();
            public Bindable<Colour4> Current
            {
                get => current.Current;
                set => current.Current = value;
            }

            public ColorPicker()
            {
                AutoSizeAxes = Axes.Y;
                Child = textBox = new LabelledColorTextBox
                {
                    RelativeSizeAxes = Axes.X,
                    CommitOnFocusLost = true,
                };

                textBox.CurrentColor.BindTo(Current);
            }
        }

        private class LabelledColorTextBox : LabelledTextBox
        {
            private Box box;
            private ColorTextBox textBox;
            public readonly Bindable<Colour4> CurrentColor = new Bindable<Colour4>();

            public LabelledColorTextBox()
            {
                textBox.CurrentColor.BindTo(CurrentColor);
                CurrentColor.ValueChanged += e => box.Colour = e.NewValue;
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                box.Colour = CurrentColor.Value;
            }

            protected override TextBox CreateTextBox() => textBox = new ColorTextBox
            {
                RelativeSizeAxes = Axes.X,
            };

            protected override Drawable CreateLabel() => new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Margin = new MarginPadding { Horizontal = 8 },
                Spacing = new Vector2(5, 0),
                Direction = FillDirection.Horizontal,
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(15),
                        Masking = true,
                        CornerRadius = 3,
                        Child = box = new Box
                        {
                            RelativeSizeAxes = Axes.Both
                        }
                    },
                    new VignetteSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = VignetteFont.Bold.With(size: 14),
                        Text = @"#",
                    }
                }
            };

            private class ColorTextBox : VignetteTextBox
            {
                public readonly Bindable<Colour4> CurrentColor = new Bindable<Colour4>();

                public ColorTextBox()
                {
                    PlaceholderText = @"000000";
                    LengthLimit = 6;

                    CurrentColor.ValueChanged += e => Text = Text.PadRight(6, '0');
                }

                protected override void LoadComplete()
                {
                    base.LoadComplete();

                    string toHexString(float f) => BitConverter.ToInt32(BitConverter.GetBytes(f), 0).ToString("X2");

                    var c = CurrentColor.Value;
                    Text = toHexString(c.R) + toHexString(c.G) + toHexString(c.B);
                }

                protected override void OnTextCommitted(bool changed) => CurrentColor.Value = Colour4.FromHex(Text.PadRight(6, '0'));

                protected override bool CanAddCharacter(char c) => @"0123456789abcdef".Contains(char.ToLowerInvariant(c));
            }
        }
    }
}