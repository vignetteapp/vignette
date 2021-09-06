// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Screens;

namespace Vignette.Game.Settings
{
    public class SettingsSidebar : Container<SettingsSidebarButton>
    {
        public const float WIDTH = 50;

        public Action OnBack
        {
            set => backButton.Action = value;
        }

        public Action OnExit
        {
            set => exitButton.Action = value;
        }

        protected override Container<SettingsSidebarButton> Content => buttonFlow;

        private readonly FluentButton backButton;
        private readonly FluentButton exitButton;
        private readonly FillFlowContainer<SettingsSidebarButton> buttonFlow;

        public SettingsSidebar()
        {
            Width = WIDTH;
            RelativeSizeAxes = Axes.Y;
            InternalChildren = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(5),
                    Children = new Drawable[]
                    {
                        backButton = new FluentButton
                        {
                            Size = new Vector2(40),
                            Icon = SegoeFluent.ArrowLeft,
                            Style = ButtonStyle.Text,
                            IconSize = 12,
                        },
                        buttonFlow = new FillFlowContainer<SettingsSidebarButton>
                        {
                            Margin = new MarginPadding { Top = 60 },
                            Spacing = new Vector2(0, 4),
                            Direction = FillDirection.Vertical,
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                        },
                        exitButton = new FluentButton
                        {
                            Size = new Vector2(40),
                            Icon = SegoeFluent.Dismiss,
                            Style = ButtonStyle.Text,
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            IconSize = 12,
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(VignetteGame game)
        {
            if (game == null)
                return;

            game.ScreenStack.ScreenPushed += (_, screen) => exitButton.FadeTo(screen is StageScreen ? 1 : 0);
        }
    }
}
