// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneButtons : TestSceneInterface
    {
        public TestSceneButtons()
        {
            AddComponentRange(new Drawable[]
            {
                new ButtonText
                {
                    Text = @"hello world",
                    Width = 200,
                    Action = () => {},
                },
                new ButtonText
                {
                    Text = @"hello world",
                    Width = 200,
                    Style = ButtonStyle.NoFill,
                    Action = () => {},
                },
                new ButtonIcon
                {
                    Icon = FontAwesome.Solid.ThumbsUp,
                    Width = 200,
                    Height = 35,
                    IconSize = new Vector2(16),
                    Action = () => {},
                },
                new ButtonIcon
                {
                    Icon = FontAwesome.Solid.ThumbsUp,
                    Style = ButtonStyle.NoFill,
                    Width = 200,
                    Height = 35,
                    IconSize = new Vector2(16),
                    Action = () => {},
                },
            });
        }
    }
}
