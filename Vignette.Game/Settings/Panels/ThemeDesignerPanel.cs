// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Panels
{
    public class ThemeDesignerPanel : SettingsSubPanel
    {
        [Cached]
        private readonly Bindable<Theme> theme;

        public ThemeDesignerPanel(IThemeSource source)
        {
            theme = new Bindable<Theme>(source.Current.Value);
            /*Children = new Drawable[]
            {
                new PreviewSection(),
                new CustomizationSection(),
            };*/
        }

        private class PreviewSection : SettingsSection
        {
            public override LocalisableString Label => "Preview";

            private ThemeProvidingContainer provider;

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> theme)
            {
                Child = provider = new ThemeProvidingContainer
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 30,
                    Child = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Padding = new MarginPadding(5),
                        Spacing = new Vector2(5),
                        Children = Enum.GetValues<ThemeSlot>()
                            .Where(s => s.GetDescription().StartsWith("theme"))
                            .Select(s => new PreviewCircle { Colour = s }).ToList(),
                    },
                };

                provider.Current.BindTo(theme);
            }

            private class PreviewCircle : ThemableEffectBox
            {
                public PreviewCircle()
                {
                    Size = new Vector2(20);
                    Shadow = true;
                    CornerRadius = 10;
                }
            }
        }

        private class CustomizationSection : SettingsSection
        {
            public override LocalisableString Label => "Customization";

            [Resolved]
            private Bindable<Theme> theme { get; set; }

            [Resolved]
            private ThemeManagingContainer manager { get; set; }

            private SettingsTextBox name;
            private SettingsColourPicker accent;

            [BackgroundDependencyLoader]
            private void load()
            {
                Children = new Drawable[]
                {
                    new SettingsSubSection
                    {
                        Children = new Drawable[]
                        {
                            name = new SettingsTextBox
                            {
                                Label = "Name",
                                Current = { Value = $"{theme.Value.Name} (Modified)" },
                                Description = "Give your theme an awesome name!",
                            },
                            accent = new SettingsColourPicker
                            {
                                Label = "Accent",
                                Current = { Value = theme.Value.Get(ThemeSlot.AccentPrimary) },
                            },
                            new FillFlowContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Direction = FillDirection.Horizontal,
                                Spacing = new Vector2(5, 0),
                                Margin = new MarginPadding { Top = 10 },
                                Children = new Drawable[]
                                {
                                    new FluentButton
                                    {
                                        Text = "Apply",
                                        Width = 90,
                                        Style = ButtonStyle.Primary,
                                        Anchor = Anchor.TopRight,
                                        Origin = Anchor.TopRight,
                                        Action = () => { },
                                    },
                                    new FluentButton
                                    {
                                        Text = "Export",
                                        Width = 90,
                                        Style = ButtonStyle.Text,
                                        Anchor = Anchor.TopRight,
                                        Origin = Anchor.TopRight,
                                        Action = handleExport,
                                    },
                                }
                            },
                        }
                    },
                };

                accent.Current.BindValueChanged(_ => theme.Value = Theme.From(accent.Current.Value).Merge(theme.Value), true);
            }

            private void handleExport()
            {
                string fileName = $"{name.Current.Value}.json";

                using var stream = manager.Store.GetStream(fileName, FileAccess.Write);
                using var writer = new StreamWriter(stream);
                stream.SetLength(0);
                writer.Write(JsonConvert.SerializeObject(theme.Value, Formatting.Indented));

                manager.Store.OpenInNativeExplorer();
            }
        }
    }
}
