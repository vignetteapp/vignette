using System.Collections.Generic;
using System.Linq;
using vignette.Configuration;
using vignette.Graphics.Interface;
using vignette.Graphics.Sprites;
using vignette.IO;
using vignette.IO.Imports;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace vignette.Overlays.Settings.Sections.Appearance
{
    public class BackgroundSettings : SettingsSubsection
    {
        public override string Header => @"Background Settings";

        [Resolved]
        private IReadOnlyList<Importer> importers { get; set; }
        private BindableList<FileMetadata> imported => importers.Where(i => i.GetType() == typeof(BackgroundImporter)).FirstOrDefault()?.Imported;

        private SettingsDropdown<string> backgrounds;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            Add(backgrounds = new SettingsDropdown<string>
            {
                Label = "Background",
                Bindable = config.GetBindable<string>(VignetteSetting.BackgroundImage),
            });

            Add(new SettingsColorPicker
            {
                Label = @"Background Color",
                Bindable = config.GetBindable<Colour4>(VignetteSetting.BackgroundColor),
            });

            Add(new SettingsSliderBar<float>
            {
                Label = @"Scale",
                Bindable = config.GetBindable<float>(VignetteSetting.BackgroundScale),
            });

            Add(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Spacing = new Vector2(0, 5),
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new VignetteSpriteText
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
                                    Bindable = config.GetBindable<float>(VignetteSetting.BackgroundPositionX),
                                },
                                new SettingsBadgedNumberBox<float>
                                {
                                    Width = 0.75f,
                                    BadgeText = @"Y",
                                    Bindable = config.GetBindable<float>(VignetteSetting.BackgroundPositionY),
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                },
                            },
                        },
                    }
                }
            });

            Add(new VignetteButton
            {
                Text = @"Reset",
                BackgroundColor = Colour4.Red,
                RelativeSizeAxes = Axes.X,
            });

            imported.CollectionChanged += (_, __) => updateList();
            updateList();
        }

        private void updateList()
        {
            var list = new List<string>(new[] { "Color" });
            list.AddRange(((IEnumerable<FileMetadata>)imported)?.Select(f => f.Path));

            Schedule(() => backgrounds.Items = list);
        }
    }
}