// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
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
        private ThemableTextFlowContainer disclaimer;

        [Resolved(canBeNull: true)]
        private SettingsOverlay settings { get; set; }

        [BackgroundDependencyLoader]
        private void load(SessionConfigManager session)
        {
            AddRangeInternal(new Drawable[]
            {
                new StageBackground(),
                new Avatar(),
                strip = new EditingStrip(),
                new Container
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Margin = new MarginPadding { Bottom = 40, Right = 20 },
                    AutoSizeAxes = Axes.Both,
                    Masking = true,
                    CornerRadius = 5,
                    Children = new Drawable[]
                    {
                        new ThemableBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = ThemeSlot.WarningBackground,
                        },
                        new ThemableSpriteIcon
                        {
                            Size = new Vector2(24),
                            Icon = VignetteFont.Logo,
                            Margin = new MarginPadding { Left = 10 },
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Colour = ThemeSlot.Warning,
                        },
                        disclaimer = new ThemableTextFlowContainer(t => t.Colour = ThemeSlot.Warning)
                        {
                            AutoSizeAxes = Axes.Both,
                            Padding = new MarginPadding(5),
                            Margin = new MarginPadding { Left = 38, Right = 5 },
                            TextAnchor = Anchor.CentreLeft,
                        },
                    },
                },
            });

            disclaimer.AddText("Vignette Debut", s => s.Font = SegoeUI.Bold);
            disclaimer.NewLine();
            disclaimer.AddText("This is a technical preview.");
            disclaimer.NewLine();
            disclaimer.AddText("Some features are not available in this build.");

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
