// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using osuTK;
using Vignette.Game.Graphics.Shapes;
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
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new ThemableBox
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = ThemeSlot.White,
                                },
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
                }
            };
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
    }
}
