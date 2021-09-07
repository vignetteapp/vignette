// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Panels
{
    public class ConfirmationPanel : SettingsSubPanel
    {
        [Resolved]
        private SettingsOverlay overlay { get; set; }

        private readonly LocalisableString message;
        private readonly Action confirmationAction;

        public ConfirmationPanel(LocalisableString message, Action confirmationAction)
        {
            this.message = message;
            this.confirmationAction = confirmationAction;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Background.Colour = ThemeSlot.Gray30;
        }

        protected override Drawable CreateContent() => new FillFlowContainer
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0, 60),
            Children = new Drawable[]
            {
                new ThemableSpriteText
                {
                    Text = message,
                    Font = SegoeUI.Bold,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = ThemeSlot.Black,
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        new FluentButton
                        {
                            Text = "OK",
                            Width = 90,
                            Style = ButtonStyle.Primary,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Action = confirmationAction,
                        },
                        new FluentButton
                        {
                            Text = "Cancel",
                            Width = 90,
                            Style = ButtonStyle.Text,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Action = () => overlay.Back(),
                        },
                    }
                }
            }
        };

        protected override void PopIn()
        {
            base.PopIn();
            overlay.BackButtonEnabled = false;
        }

        protected override void PopOut()
        {
            base.PopOut();
            overlay.BackButtonEnabled = true;
        }
    }
}
