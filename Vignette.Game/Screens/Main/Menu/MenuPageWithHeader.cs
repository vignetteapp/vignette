// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Screens.Main.Menu
{
    public abstract class MenuPageWithHeader : MenuPage
    {
        private readonly Container content;

        protected override Container<Drawable> Content => content;

        public MenuPageWithHeader()
        {
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Name = "Header",
                    Height = 300,
                    RelativeSizeAxes = Axes.X,
                    Colour = ColourInfo.GradientVertical(Colour4.White, Colour4.Transparent),
                    Child = CreateHeader(),
                },
                content = new Container
                {
                    Name = "Content",
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Top = 350 },
                },
            };
        }

        protected abstract Drawable CreateHeader();
    }
}
