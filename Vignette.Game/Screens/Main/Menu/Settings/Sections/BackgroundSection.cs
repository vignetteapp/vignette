// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Localisation;
using Vignette.Game.Configuration;

namespace Vignette.Game.Screens.Main.Menu.Settings.Sections
{
    public class BackgroundSection : SettingsSection
    {
        public override LocalisableString Header => "Background";

        private Bindable<Colour4> colour;

        private Box previewBox;

        private SettingsSlider<float> red;
        private SettingsSlider<float> blue;
        private SettingsSlider<float> green;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            AddRange(new Drawable[]
            {
                new Container
                {
                    Height = 100,
                    Masking = true,
                    CornerRadius = 2.5f,
                    RelativeSizeAxes = Axes.X,
                    Child = previewBox = new Box
                    {
                        Colour = Colour4.Red,
                        RelativeSizeAxes = Axes.Both,
                    }
                },
                new SettingsHexColourBox
                {
                    Label = "Hex",
                    Current = colour,
                },
                red = new SettingsSlider<float>
                {
                    Label = "Red",
                    Current = new BindableFloat
                    {
                        Value = colour.Value.R,
                        MinValue = 0,
                        MaxValue = 1,
                    },
                },
                green = new SettingsSlider<float>
                {
                    Label = "Green",
                    Current = new BindableFloat
                    {
                        Value = colour.Value.G,
                        MinValue = 0,
                        MaxValue = 1,
                    },
                },
                blue = new SettingsSlider<float>
                {
                    Label = "Blue",
                    Current = new BindableFloat
                    {
                        Value = colour.Value.B,
                        MinValue = 0,
                        MaxValue = 1,
                    },
                },
            });

            red.Current.ValueChanged += e => colour.Value = new Colour4(e.NewValue, colour.Value.G, colour.Value.B, 1);
            green.Current.ValueChanged += e => colour.Value = new Colour4(colour.Value.R, e.NewValue, colour.Value.B, 1);
            blue.Current.ValueChanged += e => colour.Value = new Colour4(colour.Value.R, colour.Value.G, e.NewValue, 1);

            colour.BindValueChanged(e =>
            {
                previewBox.Colour = e.NewValue;
                red.Current.Value = e.NewValue.R;
                green.Current.Value = e.NewValue.G;
                blue.Current.Value = e.NewValue.B;
            }, true);
        }
    }
}
