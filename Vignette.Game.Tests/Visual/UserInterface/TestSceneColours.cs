// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Themes;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneColours : ThemeProvidedTestScene
    {
        public TestSceneColours()
        {
            Add(new BasicScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new ColourFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    AutoSizeAxes = Axes.Y,
                    Spacing = new Vector2(0, 20),
                    Width = 200,
                },
            });
        }

        private class ColourFlowContainer : FillFlowContainer
        {
            private Bindable<Theme> theme;

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> theme)
            {
                this.theme = theme.GetBoundCopy();
                this.theme.BindValueChanged(e =>
                {
                    Clear();

                    foreach (var prop in typeof(Theme).GetProperties())
                    {
                        var value = prop.GetValue(e.NewValue);
                        Debug.Assert(value != null);

                        if (value is Colour4 colour)
                            Add(new NamedColour(prop.Name, colour));
                    }
                }, true);
            }
        }

        private class NamedColour : FillFlowContainer
        {
            public NamedColour(string name, Colour4 colour)
            {
                Size = new Vector2(200, 40);
                Direction = FillDirection.Horizontal;
                Children = new Drawable[]
                {
                    new Container
                    {
                        Size = new Vector2(160, 40),
                        Padding = new MarginPadding { Right = 20 },
                        Child = new SpriteText
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Text = name,
                        }
                    },
                    new Box
                    {
                        Size = new Vector2(40),
                        Colour = colour,
                    },
                };
            }
        }
    }
}
