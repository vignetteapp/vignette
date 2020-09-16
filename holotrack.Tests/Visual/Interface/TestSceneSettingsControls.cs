using holotrack.Overlays.Settings;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneSettingsControls : TestScene
    {
        public TestSceneSettingsControls()
        {
            Add(new FillFlowContainer
            {
                Width = 200,
                Spacing = new Vector2(0, 5),
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    new SettingsTextBox
                    {
                        Label = "Textbox",
                        Bindable = new Bindable<string>(),
                    },
                    new SettingsBadgedTextBox
                    {
                        Label = "Labelled",
                        BadgeText = "Badge",
                        Bindable = new Bindable<string>(),
                    },
                    new SettingsNumberBox<float>
                    {
                        Label = "Float Number",
                        Bindable = new Bindable<float>(),
                    },
                    new SettingsNumberBox<int>
                    {
                        Label = "Integer Number",
                        Bindable = new Bindable<int>(),
                    },
                    new SettingsBadgedNumberBox<float>
                    {
                        Label = "Labelled Number",
                        BadgeText = "float",
                        Bindable = new Bindable<float>(),
                    },
                    new SettingsColorPicker
                    {
                        Label = "Color Picker",
                        Bindable = new Bindable<Colour4> { Value = Colour4.Black },
                    },
                    new SettingsSliderBar<float>
                    {
                        Label = "Slider Bar",
                        Bindable = new BindableFloat
                        {
                            MinValue = 0,
                            MaxValue = 1,
                        }
                    },
                    new SettingsCheckbox
                    {
                        Label = "Checkbox",
                        Bindable = new Bindable<bool>(),
                    },
                    new SettingsCheckbox
                    {
                        Label = "Checkbox 2",
                        Bindable = new Bindable<bool>(true),
                    },
                    new SettingsDropdown<string>
                    {
                        Label = "String Dropdown",
                        Bindable = new Bindable<string>(),
                        Items = new [] { "Hello", "World" },
                    },
                }
            });
        }
    }
}