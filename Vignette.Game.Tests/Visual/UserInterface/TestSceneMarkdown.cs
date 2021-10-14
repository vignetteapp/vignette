// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneMarkdown : UserInterfaceTestScene
    {
        public TestSceneMarkdown()
        {
            string text = string.Join(
                Environment.NewLine,
                "# Heading 1",
                "## Heading 2",
                "### Heading 3",
                "#### Heading 4",
                "##### Heading 5",
                "###### Heading 6",
                "",
                "Paragraph",
                "",
                "**Bold**",
                "*Italic*",
                "1. Ordered Item",
                "2. Ordered Item",
                "",
                "- Unordered Item",
                "- Unordered Item",
                "",
                "[Link](https://github.com/vignette-project)",
                "",
                "![Image](https://avatars.githubusercontent.com/u/69518398)"
            );

            Add(new FluentMarkdownContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Text = text,
            });
        }
    }
}
