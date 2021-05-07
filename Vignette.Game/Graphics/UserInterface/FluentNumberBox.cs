// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A text input variant that only allows numeric input with buttons to increment or decrement the value.
    /// </summary>
    public class FluentNumberBox<T> : FluentTextInput, IHasCurrentValue<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private readonly FluentButton incrementButton;

        private readonly FluentButton decrementButton;

        private readonly BindableNumberWithCurrent<T> current = new BindableNumberWithCurrent<T>();

        public new Bindable<T> Current
        {
            get => current.Current;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (!value.Disabled)
                    Input.Current.Value = value.Value.ToString();

                current.Current = value;
            }
        }

        public FluentNumberBox()
        {
            CommitOnFocusLost = true;

            AddInternal(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(),
                    new Dimension(GridSizeMode.Absolute, 32),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        Input,
                        new FillFlowContainer
                        {
                            Padding = new MarginPadding(1),
                            Direction = FillDirection.Vertical,
                            RelativeSizeAxes = Axes.Both,
                            Children = new[]
                            {
                                incrementButton = new FluentButton
                                {
                                    Icon = FluentSystemIcons.ChevronUp16,
                                    IconSize = 8,
                                    Style = ButtonStyle.Text,
                                    RelativeSizeAxes = Axes.X,
                                    Height = 15,
                                    Action = Increment,
                                },
                                decrementButton = new FluentButton
                                {
                                    Icon = FluentSystemIcons.ChevronDown16,
                                    IconSize = 8,
                                    Style = ButtonStyle.Text,
                                    RelativeSizeAxes = Axes.X,
                                    Height = 15,
                                    Action = Decrement,
                                },
                            }
                        }
                    }
                }
            });


            Input.OnCommit += (_, __) => current.Value = !string.IsNullOrEmpty(Text) ? (T)Convert.ChangeType(Text, typeof(T)) : default;

            Current.BindValueChanged(e => Text = e.NewValue.ToString(), true);
            Current.BindDisabledChanged(e =>
            {
                Input.Current.Disabled = e;
                incrementButton.Enabled.Value = !e;
                decrementButton.Enabled.Value = !e;
            }, true);
        }

        public void Increment()
            => updateValue(1);

        public void Decrement()
            => updateValue(-1);

        private void updateValue(float multiplier)
        {
            float num = Convert.ToSingle(Text);
            float precision = Convert.ToSingle(current.Precision);

            num += precision * multiplier;

            if (current.HasDefinedRange)
            {
                float min = Convert.ToSingle(current.MinValue);
                float max = Convert.ToSingle(current.MaxValue);
                num = Math.Min(Math.Max(num, max), min);
            }

            current.Value = (T)Convert.ChangeType(num, typeof(T));
        }

        protected override TextInputContainer CreateTextBox()
            => new NumberBox();

        private class NumberBox : TextInputContainer
        {
            protected override bool CanAddCharacter(char character)
                => char.IsNumber(character) || character == '-' || character == '.';

            protected override void OnTextCommitted(bool textChanged)
            {
                if (string.IsNullOrEmpty(Current.Value))
                    Current.Value = "0";

                base.OnTextCommitted(textChanged);
            }
        }
    }
}
