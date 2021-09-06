// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneNavigationView : ThemeProvidedTestScene
    {
        private IReadOnlyList<TestItem> items => new[]
        {
            new TestItem("User", SegoeFluent.Person),
            new TestItem("Gift", SegoeFluent.Gift),
            new TestItem("Ban", SegoeFluent.Gavel),
        };

        public TestSceneNavigationView()
        {
            Children = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Relative, 0.5f),
                        new Dimension(GridSizeMode.Relative, 0.5f),
                    },
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Relative, 0.5f),
                        new Dimension(GridSizeMode.Relative, 0.5f),
                    },
                    Content = new Drawable[][]
                    {
                        new Drawable[]
                        {
                            new TestNavigationViewVertical
                            {
                                Width = 200,
                                Items = items,
                                RelativeSizeAxes = Axes.Y,
                            }
                        },
                        new Drawable[]
                        {
                            new TestNavigationViewHorizontal
                            {
                                Items = items,
                                RelativeSizeAxes = Axes.X,
                            },
                            new TestNavigationViewHorizontalWithoutIcon
                            {
                                Items = items,
                                RelativeSizeAxes = Axes.X,
                            },
                        },
                    },
                }
            };
        }

        private class TestNavigationViewVertical : NavigationVerticalTabControl<TestItem>
        {
            protected override TabItem<TestItem> CreateTabItem(TestItem value)
                => new TestNavigationViewVerticalTabItem(value);

            protected class TestNavigationViewVerticalTabItem : NavigationVerticalTabItem
            {
                protected override LocalisableString? Text => Value.Name;

                protected override IconUsage? Icon => Value.Icon;

                public TestNavigationViewVerticalTabItem(TestItem value)
                    : base(value)
                {
                }
            }
        }

        private class TestNavigationViewHorizontal : NavigationHorizontalTabControl<TestItem>
        {
            protected override TabItem<TestItem> CreateTabItem(TestItem value)
                => new TestNavigationViewHorizontalTabItem(value);

            protected class TestNavigationViewHorizontalTabItem : NavigationHorizontalTabItem
            {
                protected override LocalisableString? Text => Value.Name;

                protected override IconUsage? Icon => Value.Icon;

                public TestNavigationViewHorizontalTabItem(TestItem value)
                    : base(value)
                {
                }
            }
        }

        private class TestNavigationViewHorizontalWithoutIcon : NavigationHorizontalTabControl<TestItem>
        {
            protected override TabItem<TestItem> CreateTabItem(TestItem value)
                => new TestNavigationViewHorizontalTabItemWithoutIcon(value);

            protected class TestNavigationViewHorizontalTabItemWithoutIcon : NavigationHorizontalTabItem
            {
                protected override LocalisableString? Text => Value.Name;

                public TestNavigationViewHorizontalTabItemWithoutIcon(TestItem value)
                    : base(value)
                {
                }
            }
        }

        private struct TestItem
        {
            public string Name { get; set; }

            public IconUsage Icon { get; set; }

            public TestItem(string name, IconUsage icon)
            {
                Name = name;
                Icon = icon;
            }
        }
    }
}
