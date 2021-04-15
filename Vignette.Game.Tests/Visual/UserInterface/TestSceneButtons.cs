// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneButtons : ThemeProvidedTestScene
    {
        public TestSceneButtons()
        {
            Add(new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Both,
                Margin = new MarginPadding(10),
                Spacing = new Vector2(0, 10),
                Children = new Drawable[]
                {
                    new ButtonText
                    {
                        Text = @"Hello World",
                        Width = 200,
                        Action = () => { },
                        IsFilled = true,
                    },
                    new ButtonText
                    {
                        Text = @"Hello World",
                        Width = 200,
                        Action = () => { },
                    },
                    new ButtonIcon
                    {
                        Icon = FluentSystemIcons.Add24,
                        Width = 200,
                        Action = () => { },
                        IsFilled = true,
                    },
                    new ButtonIcon
                    {
                        Icon = FluentSystemIcons.Add24,
                        Width = 200,
                        Action = () => { },
                    },
                }
            });
        }
    }
}
