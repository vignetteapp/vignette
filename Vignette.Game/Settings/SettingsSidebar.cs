// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
        public readonly FluentButton BackButton;
        public readonly FluentButton ExitButton;

        protected override Container<SettingsSidebarButton> Content => buttonFlow;

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
                        BackButton = new FluentButton
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
                        ExitButton = new FluentButton
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

            game.ScreenStack.ScreenPushed += (_, screen) => ExitButton.FadeTo(screen is StageScreen ? 1 : 0);
        }
    }
}
