// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu
{
    public class MainMenu : Screen
    {
        private readonly Container<MenuScreen> tabView;

        private readonly MainMenuNavigation navigation;

        private SettingScreen settings;

        private MenuScreen currentScreen;

        public MainMenu()
        {
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
                                    Colour = ThemeSlot.Gray40,
                                },
                                navigation = new MainMenuNavigation
                                {
                                    Items = new MenuScreen[]
                                    {
                                        new HomeScreen(),
                                        new GuideScreen(),
                                    },
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
                                tabView = new Container<MenuScreen>
                                {
                                    RelativeSizeAxes = Axes.Both,
                                },
                            }
                        }
                    }
                }
            };

            navigation.Current.BindValueChanged(handleTabChange, true);
            navigation.OnSettings += handleSettings;
        }

        protected override void LoadComplete()
        {
            LoadComponentAsync(settings = new SettingScreen(), tabView.Add);
            base.LoadComplete();
        }

        private void handleSettings()
        {
            currentScreen?.Hide();
            navigation.Current.Value = null;

            tabView.ChangeChildDepth(settings, currentScreen?.Depth + 1 ?? 0);

            settings.Show();
            currentScreen = settings;
        }

        private void handleTabChange(ValueChangedEvent<MenuScreen> e)
        {
            currentScreen?.Hide();

            if (e.NewValue != null && !tabView.Contains(e.NewValue))
                LoadComponentAsync(e.NewValue, tabView.Add);

            if (e.NewValue != null && tabView.Contains(e.NewValue))
                tabView.ChangeChildDepth(e.NewValue, e.OldValue?.Depth + 1 ?? 0);

            e.NewValue?.Show();
            currentScreen = e.NewValue;
        }
    }
}
