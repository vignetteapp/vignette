using System;
using System.Globalization;
using vignette.Graphics;
using vignette.Graphics.Interface;
using vignette.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace vignette.Overlays.Settings
{
    public class SettingsSliderBar<T> : SettingsSliderBar<T, SliderBarWithDisplay<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
    }

    public class SettingsSliderBar<TValue, TSlider> : SettingsItem<TValue>
        where TValue : struct, IEquatable<TValue>, IComparable<TValue>, IConvertible
        where TSlider : SliderBarWithDisplay<TValue>, new()
    {
        protected override Drawable CreateControl() => new TSlider
        {
            RelativeSizeAxes = Axes.X,
        };
    }

    public class SliderBarWithDisplay<T> : GridContainer, IHasCurrentValue<T>
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        private readonly VignetteSliderBar<T> control;
        private readonly VignetteSpriteText display;

        public Bindable<T> Current
        {
            get => control.Current;
            set => control.Current = value;
        }

        public SliderBarWithDisplay()
        {
            Height = 25;
            RelativeSizeAxes = Axes.X;
            ColumnDimensions = new[]
            {
                new Dimension(GridSizeMode.AutoSize),
                new Dimension(GridSizeMode.Distributed),
            };

            Content = new[]
            {
                new Drawable[]
                {
                    display = new VignetteSpriteText
                    {
                        Font = VignetteFont.Bold,
                        Margin = new MarginPadding { Horizontal = 8 },
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    control = new VignetteSliderBar<T>
                    {
                        RelativeSizeAxes = Axes.X,
                    }
                }
            };

            control.Current.ValueChanged += (e) => display.Text = e.NewValue.ToDouble(NumberFormatInfo.InvariantInfo).ToString("0.00");
            control.Current.TriggerChange();
        }
    }
}