using System;
using System.Globalization;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Graphics.Interface
{
    public class NumberBox<T> : Container, IHasCurrentValue<T>
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        private readonly BindableWithCurrent<T> current = new BindableWithCurrent<T>();
        public Bindable<T> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        private NumberTextBox<T> textBox;
        protected virtual Drawable CreateTextBox() => textBox = new NumberTextBox<T>
        {
            RelativeSizeAxes = Axes.X,
            CommitOnFocusLost = true,
        };

        protected virtual NumberTextBox<T> TextBox => textBox;

        public NumberBox()
        {
            AutoSizeAxes = Axes.Y;

            Child = CreateTextBox();
            TextBox.CurrentNumber.BindTo(Current);
        }
    }

    public class NumberTextBox<T> : HoloTrackTextBox
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        public readonly BindableNumber<T> CurrentNumber = new BindableNumber<T>();
        private string defaultValue => default(T).ToString(NumberFormatInfo.InvariantInfo);

        public NumberTextBox()
        {
            Text = defaultValue;
        }
        protected override void OnTextCommitted(bool changed)
        {
            if (string.IsNullOrEmpty(Text))
                Text = defaultValue;

            CurrentNumber.Value = (T)Convert.ChangeType(string.IsNullOrEmpty(Text) ? defaultValue : Text, typeof(T));
            Text = CurrentNumber.Value.ToString(NumberFormatInfo.InvariantInfo);
        }

        protected override bool CanAddCharacter(char c)
        {
            var allowed = @"0123456789-";

            if (typeof(T) != typeof(int))
                allowed += ".";

            return allowed.Contains(c);
        }
    }
}