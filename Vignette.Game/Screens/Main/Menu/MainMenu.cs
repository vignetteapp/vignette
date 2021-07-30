// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Main.Menu
{
    public class MainMenu : OverlayContainer
    {
        public MenuPage CurrentPage { get; private set; }

        protected override bool StartHidden => false;

        private readonly Container<MenuPage> tabView;

        private readonly MainMenuSidePanel sidePanel;

        private readonly Spinner spinner;

        private readonly ExitButton exitButton;

        private IBindable<WindowMode> windowMode;

        public MainMenu()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            InternalChildren = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.White,
                },
                new GridContainer
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
                                    sidePanel = new MainMenuSidePanel
                                    {
                                        OnScene = () => Hide(),
                                        OnTabSelect = e => showPage(e),
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
                                                new ThemableBox
                                                {
                                                    RelativeSizeAxes = Axes.Both,
                                                    Colour = ThemeSlot.Gray10,
                                                },
                                                spinner = new Spinner
                                                {
                                                    Size = new Vector2(48),
                                                    Alpha = 0,
                                                    Anchor = Anchor.Centre,
                                                    Origin = Anchor.Centre,
                                                },
                                                tabView = new Container<MenuPage>
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
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager frameworkConfig)
        {
            windowMode = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode);
            windowMode.BindValueChanged(e => exitButton.Alpha = e.NewValue != WindowMode.Windowed ? 1 : 0, true);
        }

        /// <summary>
        /// Selects a tab from the side panel. It forces <see cref="MainMenu"/> to be the current screen if it isn't.
        /// </summary>
        /// <typeparam name="T">The <see cref="MenuPage"/> as a type to select.</typeparam>
        /// <returns>Whether the tab was successfully selected or not.</returns>
        public bool SelectTab<T>()
            where T : MenuPage
        {
            if (State.Value == Visibility.Hidden)
                Show();

            return sidePanel.SelectTab<T>();
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

        protected override void PopIn()
        {
            this
                .ScaleTo(1.1f)
                .ScaleTo(1.0f, 250, Easing.OutBack)
                .FadeInFromZero(250, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this
                .ScaleTo(1.1f, 250, Easing.OutBack)
                .FadeOutFromOne(250, Easing.OutQuint);
        }

        private void showPage(MenuPage nextPage)
        {
            CurrentPage?.Hide();

            if (nextPage != null && !tabView.Contains(nextPage))
            {
                spinner.Show();
                LoadComponentAsync(nextPage, loaded =>
                {
                    tabView.Add(loaded);
                    spinner.Hide();
                });
            }

            if (nextPage != null && tabView.Contains(nextPage))
                tabView.ChangeChildDepth(nextPage, CurrentPage?.Depth + 1 ?? 0);

            nextPage?.Show();

            CurrentPage = nextPage;
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
