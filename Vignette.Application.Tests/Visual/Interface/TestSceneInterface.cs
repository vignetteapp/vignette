// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Tests.Visual.Interface
{
    public abstract class TestSceneInterface : TestScene
    {
        private readonly FillFlowContainer content;

        private readonly BasicDropdown<Theme> themesDropdown;

        public TestSceneInterface()
        {
            AddRange(new Drawable[]
            {
                new VignetteBox { RelativeSizeAxes = Axes.Both },
                content = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 10),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                themesDropdown = new BasicDropdown<Theme>
                {
                    Margin = new MarginPadding(10),
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 200,
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load(ThemeStore store)
        {
            store.Current.BindTo(themesDropdown.Current);
            themesDropdown.ItemSource = store.Loaded;
        }

        public void AddComponent(Drawable drawable) => content.Add(drawable);

        public void AddComponentRange(IEnumerable<Drawable> range) => content.AddRange(range);
    }
}
