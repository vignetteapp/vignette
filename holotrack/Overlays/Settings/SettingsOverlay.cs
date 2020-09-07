using holotrack.Graphics;
using holotrack.Graphics.Interface;
using holotrack.Graphics.Sprites;
using holotrack.Overlays.Settings.Sections;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace holotrack.Overlays.Settings
{
    public class SettingsOverlay : HoloTrackFocusedOverlayContainer
    {
        private readonly Container<SettingsSection> sectionContent;

        public SettingsOverlay()
        {
            Size = new Vector2(700, 500);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Child = new HoloTrackForm
            {
                RelativeSizeAxes = Axes.Both,
                TitleBarVisibility = false,
                Child = new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Absolute, 200),
                        new Dimension(GridSizeMode.Distributed),
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new Container
                            {
                                Padding = new MarginPadding(15),
                                RelativeSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    new HoloTrackSpriteText
                                    {
                                        Text = @"holotrack",
                                        Font = HoloTrackFont.Black.With(size: 14),
                                    }
                                }
                            },
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        Colour = HoloTrackColor.Dark,
                                        RelativeSizeAxes = Axes.Both,
                                    },
                                    sectionContent = new Container<SettingsSection>
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Padding = new MarginPadding { Horizontal = 15, Bottom = 15, Top = 45 },
                                        Child = new AppearanceSection(),
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}