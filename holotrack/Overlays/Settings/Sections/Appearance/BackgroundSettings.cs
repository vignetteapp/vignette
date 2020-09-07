using holotrack.Configuration;
using holotrack.Graphics.Interface;
using holotrack.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace holotrack.Overlays.Settings.Sections.Appearance
{
    public class BackgroundSettings : SettingsSubsection
    {
        public override string Header => @"Background Settings";

        [BackgroundDependencyLoader]
        private void load(HoloTrackConfigManager config)
        {
            Add(new SettingsColorPicker
            {
                Label = @"Background Color",
                Bindable = config.GetBindable<Colour4>(HoloTrackSetting.BackgroundColor),
            });

            Add(new SettingsSliderBar<float>
            {
                Label = @"Scale",
                Bindable = config.GetBindable<float>(HoloTrackSetting.BackgroundScale),
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
                                    Bindable = config.GetBindable<float>(HoloTrackSetting.BackgroundPositionX),
                                },
                                new SettingsBadgedNumberBox<float>
                                {
                                    Width = 0.75f,
                                    BadgeText = @"Y",
                                    Bindable = config.GetBindable<float>(HoloTrackSetting.BackgroundPositionY),
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                },
                            },
                        },
                    }
                }
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