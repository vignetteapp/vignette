// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
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
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class AvatarSection : SettingsSection
    {
        public override LocalisableString Label => "Avatar";

        public override IconUsage Icon => SegoeFluent.Person;

        private Bindable<bool> adjust;
        private Bindable<Vector2> position;
        private SettingsSlider<float> scaleSetting;
        private SettingsSlider<float> rotationSetting;

        public event Action CalibrateAction;

        [Resolved]
        private SettingsOverlay overlay { get; set; }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, SessionConfigManager session)
        {
            adjust = session.GetBindable<bool>(SessionSetting.EditingAvatar);
            position = config.GetBindable<Vector2>(VignetteSetting.AvatarOffset);

            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Children = new Drawable[]
                    {
                        new OffsetItem(),
                        scaleSetting = new SettingsSlider<float>
                        {
                            Icon = SegoeFluent.SlideSize,
                            Label = "Scale",
                            Current = config.GetBindable<float>(VignetteSetting.AvatarScale),
                        },
                        rotationSetting = new SettingsSlider<float>
                        {
                            Icon = SegoeFluent.ArrowRotateClockwise,
                            Label = "Rotation",
                            Current = config.GetBindable<float>(VignetteSetting.AvatarRotation),
                        },
                    },
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
                        new FluentButton
                        {
                            Text = "Calibrate",
                            Width = 90,
                            Style = ButtonStyle.Secondary,
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopLeft,
                            Action = () => CalibrateAction?.Invoke(),
                        }
                    },
                },
            };
        }

        private void handleAdjustAction()
        {
            adjust.Value = true;
            overlay.RegisterBackAction(() => adjust.Value = false);
        }

        private void handleResetAction()
        {
            rotationSetting.Current.SetDefault();
            scaleSetting.Current.SetDefault();
            position.SetDefault();
        }

        private void handleCalibrateAction()
        {

        }

        private class OffsetItem : SettingsItem
        {
            private IBindable<Vector2> offset;
            private ThemableSpriteText text;

            [BackgroundDependencyLoader]
            private void load(VignetteConfigManager config)
            {
                offset = config.GetBindable<Vector2>(VignetteSetting.AvatarOffset);

                Icon = SegoeFluent.ArrowMove;
                Label = "Offset";

                LabelContainer.Add(text = new ThemableSpriteText
                {
                    Font = SegoeUI.SemiBold.With(size: 16),
                    Colour = ThemeSlot.Black,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                });

                offset.BindValueChanged(e => text.Text = e.NewValue.ToString(), true);
            }
        }
    }
}
