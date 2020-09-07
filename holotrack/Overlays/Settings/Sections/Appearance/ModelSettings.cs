using holotrack.Configuration;
using holotrack.Graphics.Interface;
using holotrack.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace holotrack.Overlays.Settings.Sections.Appearance
{
    public class ModelSettings : SettingsSubsection
    {
        [BackgroundDependencyLoader]
        private void load(HoloTrackConfigManager config)
        {
            HeaderText = @"Model Settings";
            SubHeaderText = @"Make adjustments to the Live2D model";

            Add(new SettingsSliderBar<float>
            {
                Label = @"Scale",
                Bindable = config.GetBindable<float>(HoloTrackSetting.ModelScale)
            });

            Add(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Spacing = new Vector2(0, 5),
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new HoloTrackSpriteText
                    {
                        Text = @"Position"
                    },
                    new GridContainer
                    {
                        Height = 25,
                        RelativeSizeAxes = Axes.X,
                        ColumnDimensions = new[]
                        {
                            new Dimension(GridSizeMode.Distributed),
                            new Dimension(GridSizeMode.Distributed),
                        },
                        Content = new[]
                        {
                            new Drawable[]
                            {
                                new SettingsBadgedNumberBox<float>
                                {
                                    Width = 0.75f,
                                    BadgeText = @"X",
                                    Bindable = config.GetBindable<float>(HoloTrackSetting.ModelPositionX),
                                },
                                new SettingsBadgedNumberBox<float>
                                {
                                    Width = 0.75f,
                                    BadgeText = @"Y",
                                    Bindable = config.GetBindable<float>(HoloTrackSetting.ModelPositionY),
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                },
                            },
                        },
                    }
                }
            }); 

            Add(new SettingsCheckbox
            {
                Label = @"Mouse drag moves the model",
                Bindable = config.GetBindable<bool>(HoloTrackSetting.MouseDrag),
            });

            Add(new SettingsCheckbox
            {
                Label = @"Mouse wheel scales the model",
                Bindable = config.GetBindable<bool>(HoloTrackSetting.MouseWheel),
            });

            Add(new HoloTrackButton
            {
                Text = @"Reset",
                BackgroundColor = Colour4.Red,
                RelativeSizeAxes = Axes.X,
            });
        }
    }
}