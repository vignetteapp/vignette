using System;
using holotrack.Graphics.Interface;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Overlays.Settings
{
    public class SettingsBadgedNumberBox<T> : SettingsItem<T>
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        protected override Drawable CreateControl() => new TextLabelledNumberBox<T>
        {
            RelativeSizeAxes = Axes.X
        };

        public string BadgeText
        {
            get => ((TextLabelledNumberBox<T>)Control).LabelledTextBox.Label;
            set => ((TextLabelledNumberBox<T>)Control).LabelledTextBox.Label = value;
        }

        private class TextLabelledNumberBox<T2> : NumberBox<T2>, IHasCurrentValue<T2>
            where T2 : struct, IEquatable<T2>, IComparable<T2>, IConvertible
        {
            public TextLabelledNumberTextBox<T2> LabelledTextBox { get; private set; }
            protected override Drawable CreateTextBox() => LabelledTextBox = new TextLabelledNumberTextBox<T2>
            {
                RelativeSizeAxes = Axes.X,
                CommitOnFocusLost = true,
            };

            protected override NumberTextBox<T2> TextBox => LabelledTextBox.TextBox as NumberTextBox<T2>;
        }

        private class TextLabelledNumberTextBox<TValue> : TextLabelledTextBox
            where TValue : struct, IEquatable<TValue>, IComparable<TValue>, IConvertible
        {
            protected override TextBox CreateTextBox() => new NumberTextBox<TValue>
            {
                RelativeSizeAxes = Axes.X,
            };
        }
    }
}