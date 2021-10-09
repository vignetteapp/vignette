// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Screens.Stage;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class BackdropSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.Image;

        public override LocalisableString Label => "Backdrop";

        private SettingsEnumDropdown<BackgroundType> typeSetting;
        private SettingsFileBrowser pathSetting;
        private SettingsSlider<float> scaleSetting;
        private SettingsSlider<float> rotationSetting;
        private Bindable<Vector2> position;
        private Bindable<bool> backgroundAdjust;

        [Resolved]
        private SettingsOverlay overlay { get; set; }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, SessionConfigManager session)
        {
            position = config.GetBindable<Vector2>(VignetteSetting.BackgroundPosition);
            backgroundAdjust = session.GetBindable<bool>(SessionSetting.EditingBackground);

            pathSetting = new BackdropFileBrowser
            {
                Label = "File",
                Current = config.GetBindable<string>(VignetteSetting.BackgroundPath),
            };

            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Children = new Drawable[]
                    {
                        typeSetting = new SettingsEnumDropdown<BackgroundType>
                        {
                            Label = "Type",
                            Current = config.GetBindable<BackgroundType>(VignetteSetting.BackgroundType),
                        },
                        new StatefulSettingsSubSection<BackgroundType>
                        {
                            Current = typeSetting.Current,
                            Map = new Dictionary<BackgroundType, Drawable>
                            {
                                {
                                    BackgroundType.Colour,
                                    new SettingsColourPicker
                                    {
                                        Icon = SegoeFluent.ColorBackground,
                                        Label = "Background Colour",
                                        Current = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour),
                                    }
                                },
                                { BackgroundType.Image, pathSetting },
                                { BackgroundType.Video, pathSetting },
                            }
                        }
                    }
                },
                new SettingsSubSection
                {
                    Children = new Drawable[]
                    {
                        new PositionItem(),
                        scaleSetting = new SettingsSlider<float>
                        {
                            Icon = SegoeFluent.SlideSize,
                            Label = "Scale",
                            Current = config.GetBindable<float>(VignetteSetting.BackgroundScale),
                        },
                        rotationSetting = new SettingsSlider<float>
                        {
                            Icon = SegoeFluent.ArrowRotateClockwise,
                            Label = "Rotation",
                            Current = config.GetBindable<float>(VignetteSetting.BackgroundRotation),
                        },
                    }
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5, 0),
                    Children = new Drawable[]
                    {
                        new FluentButton
                        {
                            Text = "Adjust",
                            Width = 90,
                            Style = ButtonStyle.Primary,
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Action = handleAdjustAction,
                        },
                        new FluentButton
                        {
                            Text = "Reset",
                            Width = 90,
                            Style = ButtonStyle.Text,
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Action = handleResetAction,
                        },
                    },
                },
            };
        }

        private void handleAdjustAction()
        {
            backgroundAdjust.Value = true;
            overlay.RegisterBackAction(() => backgroundAdjust.Value = false);
        }

        private void handleResetAction()
        {
            rotationSetting.Current.SetDefault();
            scaleSetting.Current.SetDefault();
            position.SetDefault();
        }

        private class PositionItem : SettingsItem
        {
            private ThemableSpriteText text;
            private Bindable<Vector2> position;

            [BackgroundDependencyLoader]
            private void load(VignetteConfigManager config)
            {
                Icon = SegoeFluent.ArrowMove;
                Label = "Position";

                LabelContainer.Add(text = new ThemableSpriteText
                {
                    Font = SegoeUI.SemiBold.With(size: 16),
                    Colour = ThemeSlot.Black,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                });

                position = config.GetBindable<Vector2>(VignetteSetting.BackgroundPosition);
                position.BindValueChanged(e => text.Text = $"{e.NewValue.X:0},{e.NewValue.Y:0}", true);
            }
        }

        private class BackdropFileBrowser : SettingsFileBrowser
        {
            protected override string[] Extensions
            {
                get
                {
                    switch (type.Value)
                    {
                        case BackgroundType.Image:
                            return new[] { ".png", ".jpeg", ".jpg" };

                        case BackgroundType.Video:
                            return new[] { ".mp4" };

                        default:
                            return Array.Empty<string>();
                    }
                }
            }

            private Bindable<BackgroundType> type;

            [BackgroundDependencyLoader]
            private void load(VignetteConfigManager config)
            {
                type = config.GetBindable<BackgroundType>(VignetteSetting.BackgroundType);
            }
        }
    }
}
