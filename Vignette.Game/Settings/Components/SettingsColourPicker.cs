// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsColourPicker : SettingsControl<ColourPickerButton, Colour4>
    {
        protected override ColourPickerButton CreateControl() => new ColourPickerButton();
    }

    public class ColourPickerButton : Button, IHasPopover, IHasCurrentValue<Colour4>
    {
        public Bindable<Colour4> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        private readonly Box previewBox;
        private readonly BindableWithCurrent<Colour4> current = new BindableWithCurrent<Colour4>(Colour4.White);

        public ColourPickerButton()
        {
            Size = new Vector2(28);
            Masking = true;
            CornerRadius = 5;
            Child = previewBox = new Box
            {
                RelativeSizeAxes = Axes.Both,
            };

            Action = () => this.ShowPopover();
            Current.BindValueChanged(e => previewBox.Colour = e.NewValue, true);
        }

        public Popover GetPopover() => new FluentPopover
        {
            Child = new FluentColourPicker
            {
                Scale = new Vector2(0.6f),
                Current = { BindTarget = Current },
            },
        };
    }
}
