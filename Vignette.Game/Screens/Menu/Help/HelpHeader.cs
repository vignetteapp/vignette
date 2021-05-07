// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Help
{
    public class HelpHeader : Container
    {
        public FluentSearchBox SearchBox { get; private set; }

        public HelpHeader()
        {
            Masking = true;
            RelativeSizeAxes = Axes.X;
            Children = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.AccentSecondary,
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Padding = new MarginPadding { Left = 30 },
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new ThemableSpriteText
                        {
                            Text = "Knowledgebase",
                            Font = SegoeUI.Bold.With(size: 32),
                            Colour = ThemeSlot.Black,
                        },
                        new ThemableSpriteText
                        {
                            Text = "Learn Vignette",
                            Font = SegoeUI.Regular.With(size: 16),
                            Colour = ThemeSlot.Black,
                        },
                        SearchBox = new FluentSearchBox
                        {
                            Width = 500,
                            Style = TextBoxStyle.Borderless,
                            Margin = new MarginPadding { Top = 20 },
                        }
                    },
                },
            };
        }
    }
}
