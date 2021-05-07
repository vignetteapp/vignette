// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Screens.Menu.Settings;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu
{
    public abstract class SettingsScreen : MenuScreen
    {
        protected override Container<Drawable> Content => sectionFlow;

        private SearchContainer sectionFlow;

        private FluentScrollContainer scrollContainer;

        private SettingsNavigation navigation;

        private FluentSearchBox searchBox;

        public SettingsScreen()
        {
            InternalChild = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(),
                    new Dimension(GridSizeMode.AutoSize),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                new ThemableBox
                                {
                                    Height = 2,
                                    RelativeSizeAxes = Axes.X,
                                    Colour = ThemeSlot.Gray30,
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                },
                                navigation = new SettingsNavigation
                                {
                                    RelativeSizeAxes = Axes.X,
                                },
                                searchBox = new FluentSearchBox
                                {
                                    Margin = new MarginPadding { Right = 10 },
                                    Anchor = Anchor.CentreRight,
                                    Origin = Anchor.CentreRight,
                                    Width = 200,
                                    Style = TextBoxStyle.Underlined,
                                },
                            }
                        }
                    },
                    new Drawable[]
                    {
                        scrollContainer = new FluentScrollContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = sectionFlow = new SearchContainer
                            {
                                Width = 0.8f,
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                            },
                        }
                    },
                    new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                new ThemableBox
                                {
                                    Height = 2,
                                    RelativeSizeAxes = Axes.X,
                                    Colour = ThemeSlot.Gray30,
                                },
                                new FillFlowContainer
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Padding = new MarginPadding { Vertical = 10, Right = 20 },
                                    Direction = FillDirection.Horizontal,
                                    Spacing = new Vector2(10, 0),
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                    Children = new Drawable[]
                                    {
                                        new FluentButton
                                        {
                                            Text = "Scroll to Top",
                                            Style = ButtonStyle.Text,
                                            AutoSizeAxes = Axes.X,
                                            Action = () => scrollContainer.ScrollToStart(),
                                        },
                                    },
                                }
                            }
                        }
                    },
                }
            };

            navigation.Current.ValueChanged += e =>
            {
                if (!hasInitialized)
                {
                    hasInitialized = true;
                    return;
                }

                if (lastSection != e.NewValue)
                {
                    lastSection = e.NewValue;

                    if (lastSection != null)
                        scrollContainer.ScrollTo(lastSection);

                    lastSection?.Highlight();
                }
            };

            selectedSection.ValueChanged += e =>
            {
                if (lastSection != e.NewValue)
                {
                    lastSection = e.NewValue;
                    navigation.Current.Value = lastSection;
                }
            };

            searchBox.Current.ValueChanged += e => sectionFlow.SearchTerm = e.NewValue;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            navigation.Items = sectionFlow.Children.OfType<SettingsSection>().ToArray();
        }

        private float? lastKnownScroll;

        private SettingsSection lastSection;

        private readonly Bindable<SettingsSection> selectedSection = new Bindable<SettingsSection>();

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            float currentScroll = scrollContainer.Current;
            if (currentScroll != lastKnownScroll)
            {
                lastKnownScroll = currentScroll;

                selectedSection.Value = Children.OfType<SettingsSection>()
                                            .TakeWhile(section => scrollContainer.GetChildPosInContent(section) - currentScroll - 10 <= 0)
                                            .LastOrDefault() ?? Children.OfType<SettingsSection>().FirstOrDefault();
            }
        }

        private bool hasInitialized;

        private class SettingsNavigation : NavigationViewHorizontal<SettingsSection>
        {
            protected override TabItem<SettingsSection> CreateTabItem(SettingsSection value)
                    => new SettingsNavigationTabItem(value);

            private class SettingsNavigationTabItem : NavigationViewHorizontalTabItem
            {
                protected override LocalisableString? Text => Value.Header;

                public SettingsNavigationTabItem(SettingsSection value)
                    : base(value)
                {
                }
            }
        }
    }
}
