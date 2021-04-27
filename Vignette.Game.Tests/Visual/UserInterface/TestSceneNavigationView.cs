// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneNavigationView : ThemeProvidedTestScene
    {
        private IReadOnlyList<TestItemWithIcon> items => new[]
        {
            new TestItemWithIcon("User", FluentSystemIcons.Person24),
            new TestItemWithIcon("Gift", FluentSystemIcons.Gift24),
            new TestItemWithIcon("Ban", FluentSystemIcons.Gavel24),
        };

        public TestSceneNavigationView()
        {
            Child = new GridContainer
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
                        new NavigationViewVertical<TestItemWithIcon>
                        {
                            Items = items
                        }
                    },
                    new Drawable[]
                    {
                        new NavigationViewHorizontal<TestItemWithIcon>
                        {
                            Items = items
                        },
                        new NavigationViewHorizontal<TestItem>
                        {
                            Items = new[]
                            {
                                new TestItem("User"),
                                new TestItem("Gift"),
                                new TestItem("Ban"),
                            }
                        },
                    },
                },
            };
        }

        private class TestItem : Drawable, IHasText
        {
            private LocalisableString text;

            public LocalisableString Text
            {
                get => text;
                set => text = value;
            }

            public TestItem(string text)
            {
                Text = text;
            }
        }

        private class TestItemWithIcon : TestItem, IHasIcon
        {
            private IconUsage icon;

            public IconUsage Icon
            {
                get => icon;
                set => icon = value;
            }

            public TestItemWithIcon(string text, IconUsage icon)
                : base(text)
            {
                Icon = icon;
            }
        }
    }
}
