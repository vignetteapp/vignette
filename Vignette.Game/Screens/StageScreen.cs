// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Screens.Stage;
using Vignette.Game.Screens.Stage.Edit;
using Vignette.Game.Settings;
using Vignette.Game.Settings.Sections;

namespace Vignette.Game.Screens
{
    public class StageScreen : VignetteScreen, IHasContextMenu
    {
        protected override bool ShowSettingsOneEnter => true;

        private EditingStrip strip;
        private Bindable<bool> backgroundAdjust;

        [Resolved(canBeNull: true)]
        private SettingsOverlay settings { get; set; }

        [BackgroundDependencyLoader]
        private void load(SessionConfigManager session)
        {
            AddRangeInternal(new Drawable[]
            {
                new StageBackground(),
                strip = new EditingStrip(),
            });

            backgroundAdjust = session.GetBindable<bool>(SessionSetting.EditingBackground);
            backgroundAdjust.BindValueChanged(_ => handleEditingState(), true);
        }

        private void handleEditingState()
        {
            if (backgroundAdjust.Value)
            {
                settings.KeepBodyHidden = true;
                settings?.HideBody();
                strip.Show();
            }
            else
            {
                settings.KeepBodyHidden = false;
                settings?.ShowBody();
                strip.Hide();
            }
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeOut().Delay(500).FadeInFromZero(500, Easing.OutQuint);
        }

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new FluentMenuItem("Avatar", () => settings?.ScrollTo<AvatarSection>()),
            new FluentMenuItem("Backdrop", () => settings?.ScrollTo<BackdropSection>()),
            new FluentMenuItem("Camera", () => settings?.ScrollTo<RecognitionSection>()),
            new FluentMenuDivider(),
            new FluentMenuItem("Settings", () => settings?.Show()),
        };
    }
}
