// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.IO;

namespace Vignette.Application.Tests.Visual.Interface
{
    public abstract class TestSceneInterface : TestScene
    {
        private readonly FillFlowContainer content;

        private readonly BasicDropdown<string> themesDropdown;

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
                themesDropdown = new BasicDropdown<string>
                {
                    Margin = new MarginPadding(10),
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 200,
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig, ThemeStore store)
        {
            themesDropdown.Current = appConfig.GetBindable<string>(ApplicationConfig.Theme);
            store.Loaded.BindCollectionChanged((s, e) => Schedule(() => themesDropdown.Items = store.Loaded.Select(t => t.Name)), true);
        }

        public void AddComponent(Drawable drawable) => content.Add(drawable);

        public void AddComponentRange(IEnumerable<Drawable> range) => content.AddRange(range);
    }
}
