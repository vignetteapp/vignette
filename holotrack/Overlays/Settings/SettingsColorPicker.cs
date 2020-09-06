using holotrack.Graphics;
using holotrack.Graphics.Interface;
using holotrack.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Overlays.Settings
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

                textBox.CurrentValue.BindTo(Current);
            }
        }

        private class LabelledColorTextBox : LabelledTextBox
        {
            private Box box;
            public readonly Bindable<Colour4> CurrentValue = new Bindable<Colour4>();

            public LabelledColorTextBox()
            {
                TextBox.OnCommit += (sender, changed) => box.Colour = CurrentValue.Value = Colour4.FromHex(Text.PadRight(6, '0'));
            }

            protected override TextBox CreateTextBox() => new ColorTextBox
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
                    new HoloTrackSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = HoloTrackFont.Bold.With(size: 14),
                        Text = @"#",
                    }
                }
            };

            private class ColorTextBox : HoloTrackTextBox
            {
                public ColorTextBox()
                {
                    Text = @"FFFFFF";
                    PlaceholderText = @"000000";
                    LengthLimit = 6;
                }

                protected override void OnTextCommitted(bool changed) => Text = Text.PadRight(6, '0');

                protected override bool CanAddCharacter(char c) => @"0123456789abcdef".Contains(char.ToLowerInvariant(c));
            }
        }
    }
}