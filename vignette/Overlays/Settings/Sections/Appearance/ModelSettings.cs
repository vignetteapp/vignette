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
    public class ModelSettings : SettingsSubsection
    {
        public override string Header => @"Model Settings";

        [Resolved]
        private IReadOnlyList<Importer> importers { get; set; }
        private BindableList<FileMetadata> imported => importers.Where(i => i.GetType() == typeof(CubismAssetImporter)).FirstOrDefault()?.Imported;

        private SettingsDropdown<string> assets;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            Add(assets = new SettingsDropdown<string>
            {
                Label = "Model",
                Bindable = config.GetBindable<string>(VignetteSetting.Model),
            });

            Add(new SettingsSliderBar<float>
            {
                Label = @"Scale",
                Bindable = config.GetBindable<float>(VignetteSetting.ModelScale)
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
                                    Bindable = config.GetBindable<float>(VignetteSetting.ModelPositionX),
                                },
                                new SettingsBadgedNumberBox<float>
                                {
                                    Width = 0.75f,
                                    BadgeText = @"Y",
                                    Bindable = config.GetBindable<float>(VignetteSetting.ModelPositionY),
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
                Bindable = config.GetBindable<bool>(VignetteSetting.MouseDrag),
            });

            Add(new SettingsCheckbox
            {
                Label = @"Mouse wheel scales the model",
                Bindable = config.GetBindable<bool>(VignetteSetting.MouseWheel),
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
            var list = new List<string>();
            list.AddRange(new[]
            {
                "haru.haru.model3.json",
                "haru_greeter.haru_greeter.model3.json",
                "hibiki.hibiki.model3.json",
                "hiyori.hiyori.model3.json",
                "shizuku.shizuku.model3.json"
            });
            list.AddRange(((IEnumerable<FileMetadata>)imported)?.Select(f => f.Path));

            Schedule(() => assets.Items = list);
        }
    }
}