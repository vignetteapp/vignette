// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
        private Bindable<bool> avatarAdjust;
        private Bindable<bool> backgroundAdjust;

        [Resolved(canBeNull: true)]
        private SettingsOverlay settings { get; set; }

        [Cached]
        private readonly TrackingComponent tracker = new TrackingComponent();

        [BackgroundDependencyLoader]
        private void load(SessionConfigManager session)
        {
            AddRangeInternal(new Drawable[]
            {
                new StageBackground(),
                new Avatar(),
                strip = new EditingStrip(),
                tracker,
            });

            backgroundAdjust = session.GetBindable<bool>(SessionSetting.EditingBackground);
            backgroundAdjust.ValueChanged += _ => handleEditingState();

            avatarAdjust = session.GetBindable<bool>(SessionSetting.EditingAvatar);
            avatarAdjust.ValueChanged += _ => handleEditingState();

            handleEditingState();
        }

        private void handleEditingState()
        {
            if (backgroundAdjust.Value || avatarAdjust.Value)
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
