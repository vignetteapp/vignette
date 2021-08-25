// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Overlays.MainMenu;
using Vignette.Game.Themeing;
using Vignette.Game.Views;

namespace Vignette.Game.Overlays
{
    public class MainMenuOverlay : OverlayContainer
    {
        private readonly GridContainer content;
        private readonly ViewContainer tabView;
        private readonly MainMenuSidePanel sidePanel;
        private readonly ExitButton exitButton;
        private IBindable<WindowMode> windowMode;

        protected override bool StartHidden => true;

        public MainMenuOverlay()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.White,
                },
                content = new GridContainer
                {
                    Scale = new Vector2(1.1f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
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
                                        OnTabSelect = e => tabView.Show(e),
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
                                                tabView = new ViewContainer
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
        /// Selects a tab from the side panel. It forces <see cref="MainMenuOverlay"/> to be the current screen if it isn't.
        /// </summary>
        /// <typeparam name="T">The <see cref="MenuPage"/> as a type to select.</typeparam>
        /// <returns>Whether the tab was successfully selected or not.</returns>
        public bool SelectTab<T>()
            where T : MenuView
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
            content.ScaleTo(1.0f, 250, Easing.OutBack);
            this.FadeInFromZero(250, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            content.ScaleTo(1.1f, 250, Easing.OutBack);
            this.FadeOutFromOne(250, Easing.OutQuint);
        }
    }
}
