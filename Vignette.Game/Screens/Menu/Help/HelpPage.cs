// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Help
{
    public class HelpPage : MenuPage
    {
        public override LocalisableString Title => "Knowledgebase";

        public override IconUsage Icon => FluentSystemIcons.Book24;

        private HelpHeader header;

        private HelpBreadcrumb breadcrumb;

        private SearchContainer articleFlow;

        public HelpPage()
        {
            Child = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        header = new HelpHeader
                        {
                            Height = 200,
                        },
                    },
                    new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Children = new Drawable[]
                            {
                                breadcrumb = new HelpBreadcrumb
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Margin = new MarginPadding { Top = 5, Left = 20 },
                                },
                                new ThemableBox
                                {
                                    Height = 2,
                                    RelativeSizeAxes = Axes.X,
                                    Colour = ThemeSlot.Gray30,
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                },
                            },
                        }
                    },
                    new Drawable[]
                    {
                        new FluentScrollContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = articleFlow = new SearchContainer
                            {
                                Width = 0.8f,
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Padding = new MarginPadding(20),
                                Spacing = new Vector2(10),
                                Direction = FillDirection.Vertical,
                            },
                        },
                    },
                },
            };

            breadcrumb.AddItem("Knowledgebase");
            breadcrumb.Current.ValueChanged += e => header.ResizeHeightTo(e.NewValue == "Knowledgebase" ? 200 : 0, 200, Easing.OutQuint);

            header.SearchBox.Current.ValueChanged += e => articleFlow.SearchTerm = e.NewValue;
        }
    }
}
