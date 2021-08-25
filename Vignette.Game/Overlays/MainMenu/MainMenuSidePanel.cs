// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Overlays.MainMenu.Settings;

namespace Vignette.Game.Overlays.MainMenu
{
    public class MainMenuSidePanel : Container, IStateful<NavigationPanelState>
    {
        public Action OnScene = null;

        public Action<MenuView> OnTabSelect;

        public MenuView SelectedTab { get; private set; }

        public IEnumerable<MenuView> Items => topNavigation.Items.Concat(botNavigation.Items);

        public event Action<NavigationPanelState> StateChanged;

        private readonly MainMenuNavigationView topNavigation;
        private readonly MainMenuNavigationView botNavigation;
        private NavigationPanelState state = NavigationPanelState.Expanded;

        public NavigationPanelState State
        {
            get => state;
            set
            {
                if (state == value)
                    return;

                state = value;

                Schedule(() => this.ResizeWidthTo(State == NavigationPanelState.Contracted ? 44 : 200, 200, Easing.OutQuint));
                StateChanged?.Invoke(State);
            }
        }

        public MainMenuSidePanel()
        {
            Width = 200;
            RelativeSizeAxes = Axes.Y;
            Add(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(),
                    new Dimension(GridSizeMode.AutoSize),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        new FluentButton
                        {
                            Size = new Vector2(44),
                            Icon = FluentSystemIcons.EyeShow24,
                            Style = ButtonStyle.Text,
                            Action = () => OnScene?.Invoke(),
                        },
                    },
                    new Drawable[]
                    {
                        new FluentButton
                        {
                            Size = new Vector2(44),
                            Icon = FluentSystemIcons.Navigation20,
                            Style = ButtonStyle.Text,
                            Action = Toggle,
                        },
                    },
                    new Drawable[]
                    {
                        topNavigation = new MainMenuNavigationView
                        {
                            SelectFirstTabByDefault = false,
                            Items = new MenuView[]
                            {
                                new HomeView(),
                                new StageSettings(),
                                new TrackerSettings(),
                                new KeybindSettings(),
                            },
                        }
                    },
                    new Drawable[]
                    {
                        botNavigation = new MainMenuNavigationView
                        {
                            SelectFirstTabByDefault = false,
                            Items = new MenuView[]
                            {
                                new GameSettings(),
                            },
                        }
                    },
                },
            });

            topNavigation.Current.ValueChanged += handleTabSelection;
            botNavigation.Current.ValueChanged += handleTabSelection;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Load the first item in the top navigation to make it ready for display as soon as the main menu is opened.
            LoadComponentAsync(topNavigation.Items[0], loaded => topNavigation.Current.Value = loaded);
        }

        /// <summary>
        /// Selects a tab.
        /// </summary>
        /// <param name="menuScreenType">The <see cref="MenuPage"/> as a type to select.</param>
        /// <returns>Whether the tab was successfully selected or not.</returns>
        public bool SelectTab<T>()
            where T : MenuView
        {
            var target = Items.FirstOrDefault(s => s?.GetType() == typeof(T));

            if (target == null)
                return false;

            if (botNavigation.Items.Contains(target))
                botNavigation.Current.Value = target;
            else
                topNavigation.Current.Value = target;

            return true;
        }

        private void handleTabSelection(ValueChangedEvent<MenuView> e)
        {
            if (e.NewValue == null)
                return;

            if (botNavigation.Items.Contains(e.NewValue))
                topNavigation.Current.Value = null;
            else
                botNavigation.Current.Value = null;

            SelectedTab = e.NewValue;
            OnTabSelect?.Invoke(SelectedTab);
        }

        /// <summary>
        /// Toggles the side panel.
        /// </summary>
        public void Toggle()
            => State = State == NavigationPanelState.Contracted ? NavigationPanelState.Expanded : NavigationPanelState.Contracted;

        /// <summary>
        /// Expands the side panel.
        /// </summary>
        public void Expand()
            => State = NavigationPanelState.Expanded;

        /// <summary>
        /// Contracts the side panel.
        /// </summary>
        public void Contract()
            => State = NavigationPanelState.Contracted;
    }

    public enum NavigationPanelState
    {
        Contracted,

        Expanded,
    }
}
