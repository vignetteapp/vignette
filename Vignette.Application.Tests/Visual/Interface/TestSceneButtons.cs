// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneButtons : TestScene
    {
        public TestSceneButtons()
        {
            Add(new FillFlowContainer
            {
                Spacing = new Vector2(0, 10),
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new ThemedTextButton
                    {
                        Text = @"hello world",
                        Width = 200,
                        Action = () => {},
                    },
                    new ThemedTextButton
                    {
                        Text = @"hello world",
                        Width = 200,
                        Style = ButtonStyle.NoFill,
                        Action = () => {},
                    },
                    new ThemedIconButton
                    {
                        Icon = FontAwesome.Solid.ThumbsUp,
                        Width = 200,
                        Height = 25,
                        IconSize = new Vector2(16),
                        Action = () => {},
                    },
                    new ThemedIconButton
                    {
                        Icon = FontAwesome.Solid.ThumbsUp,
                        Style = ButtonStyle.NoFill,
                        Width = 200,
                        Height = 25,
                        IconSize = new Vector2(16),
                        Action = () => {},
                    },
                }
            });
        }
    }
}
