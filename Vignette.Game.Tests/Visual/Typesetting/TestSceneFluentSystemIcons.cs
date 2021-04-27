// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Diagnostics;
using System.Reflection;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Tests.Visual.Typesetting
{
    public class TestSceneFluentSystemIcons : VignetteTestScene
    {
        public TestSceneFluentSystemIcons()
        {
            FillFlowContainer flow;

            Add(new BasicScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = flow = new FillFlowContainer
                {
                    Direction = FillDirection.Full,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                }
            });

            foreach (var prop in typeof(FluentSystemIcons).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                var value = prop.GetValue(null);
                Debug.Assert(value != null);

                flow.Add(new Icon(prop.Name, (IconUsage)value));
            }
        }

        private class Icon : FillFlowContainer
        {
            public Icon(string name, IconUsage icon)
            {
                Size = new Vector2(200, 40);
                Direction = FillDirection.Horizontal;
                Children = new Drawable[]
                {
                    new Container
                    {
                        Size = new Vector2(160, 40),
                        Child = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = name,
                            Font = FontUsage.Default.With(size: 14),
                        }
                    },
                    new Container
                    {
                        Size = new Vector2(40),
                        Child = new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(30),
                            Icon = icon,
                        },
                    },
                };
            }
        }
    }
}
