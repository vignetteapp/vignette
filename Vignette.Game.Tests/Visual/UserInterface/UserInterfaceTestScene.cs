// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public abstract class UserInterfaceTestScene : ThemeProvidedTestScene
    {
        private FillFlowContainer content;

        protected override Container<Drawable> Content => content;

        public UserInterfaceTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.White,
                },
                content = new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 10),
                    RelativeSizeAxes = Axes.Both,
                }
            });
        }
    }
}
