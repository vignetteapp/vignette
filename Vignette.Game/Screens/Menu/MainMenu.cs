// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Input;
using Vignette.Game.Screens.Backgrounds;
using Vignette.Game.Screens.Scene;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu
{
    [Cached]
    public class MainMenu : VignetteScreen, IKeyBindingHandler<GlobalAction>
    {
        public MenuScreen CurrentScreen { get; private set; }

        private readonly Container<MenuScreen> tabView;

        private MainMenuSidePanel sidePanel;

        private Spinner spinner;

        private ExitButton exitButton;

        private IBindable<WindowMode> windowMode;

        public MainMenu()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            InternalChild = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.Distributed),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.X,
                            RelativeSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                new ThemableBox
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = ThemeSlot.Gray10,
                                },
                                sidePanel = new MainMenuSidePanel
                                {
                                    OnScene = openScene,
                                    OnTabSelect = e => addScreen(e),
                                },
                            }
                        },
                        new GridContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            RowDimensions = new[]
                            {
                                new Dimension(GridSizeMode.AutoSize),
                                new Dimension(),
                            },
                            Content = new Drawable[][]
                            {
                                new Drawable[]
                                {
                                    exitButton = new ExitButton
                                    {
                                        Size = new Vector2(32, 24),
                                        Anchor = Anchor.TopRight,
                                        Origin = Anchor.TopRight,
                                    }
                                },
                                new Drawable[]
                                {
                                    new Container
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Children = new Drawable[]
                                        {
                                            spinner = new Spinner
                                            {
                                                Size = new Vector2(48),
                                                Alpha = 0,
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                            },
                                            tabView = new Container<MenuScreen>
                                            {
                                                RelativeSizeAxes = Axes.Both,
                                            },
                                        }
                                    }
                                }
                            },
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager frameworkConfig)
        {
            windowMode = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode);
            windowMode.BindValueChanged(e =>
            {
                exitButton.Alpha = e.NewValue != WindowMode.Windowed ? 1 : 0;
            }, true);
        }

        /// <summary>
        /// Selects a tab from the side panel. It forces <see cref="MainMenu"/> to be the current screen if it isn't.
        /// </summary>
        /// <param name="menuScreenType">The <see cref="MenuScreen"/> as a type to select.</param>
        /// <returns>Whether the tab was successfully selected or not.</returns>
        public bool SelectTab(Type menuScreenType)
        {
            if (!this.IsCurrentScreen())
                this.MakeCurrent();

            return sidePanel.SelectTab(menuScreenType);
        }

        /// <summary>
        /// Toggles the side panel's state.
        /// </summary>
        public void ToggleNavigationView()
            => sidePanel.Toggle();

        /// <summary>
        /// Expands the side panel.
        /// </summary>
        public void ExpandNavigationView()
            => sidePanel.Expand();

        /// <summary>
        /// Contracts the side panel.
        /// </summary>
        public void ContractNavigationView()
            => sidePanel.Contract();

        protected override BackgroundScreen CreateBackground()
            => new BackgroundScreenTheme();

        public override void OnResuming(IScreen last)
        {
            base.OnEntering(last);
            this
                .ScaleTo(1.1f)
                .ScaleTo(1.0f, 250, Easing.OutBack)
                .FadeInFromZero(250, Easing.OutQuint);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnExiting(next);
            this
                .ScaleTo(1.1f, 250, Easing.OutBack)
                .FadeOutFromOne(250, Easing.OutQuint);
        }

        private void addScreen(MenuScreen nextScreen)
        {
            CurrentScreen?.Hide();

            if (nextScreen != null && !tabView.Contains(nextScreen))
            {
                spinner.Show();
                LoadComponentAsync(nextScreen, loaded =>
                {
                    tabView.Add(loaded);
                    spinner.Hide();
                });
            }

            if (nextScreen != null && tabView.Contains(nextScreen))
                tabView.ChangeChildDepth(nextScreen, CurrentScreen?.Depth + 1 ?? 0);

            nextScreen?.Show();

            CurrentScreen = nextScreen;
        }

        private void openScene() => this.Push(new SceneScreen());

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleMainMenu:
                    openScene();
                    return true;
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }

        private class ExitButton : FluentButtonBase
        {
            private readonly ThemableBox background;

            public ExitButton()
            {
                BackgroundResting = ThemeSlot.Transparent;
                BackgroundHovered = ThemeSlot.Error;
                BackgroundPressed = ThemeSlot.ErrorBackground;
                BackgroundDisabled = ThemeSlot.Transparent;

                Children = new Drawable[]
                {
                    background = new ThemableBox
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    new ThemableSpriteIcon
                    {
                        Icon = FluentSystemIcons.Dismiss28,
                        Size = new Vector2(9),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = ThemeSlot.Black,
                    },
                };
            }

            protected override void UpdateBackground(ThemeSlot slot)
                => background.Colour = slot;

            [BackgroundDependencyLoader]
            private void load(GameHost host)
            {
                Action = () => host.Exit();
            }
        }
    }
}
