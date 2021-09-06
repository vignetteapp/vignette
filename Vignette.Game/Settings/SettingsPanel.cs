// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;

namespace Vignette.Game.Settings
{
    public class SettingsPanel : Container
    {
        public string SearchTerm
        {
            get => flow.SearchTerm;
            set => flow.SearchTerm = value;
        }

        public UserTrackingScrollContainer ScrollContainer { get; private set; }

        protected override Container<Drawable> Content => flow;

        private readonly SearchContainer flow;

        public SettingsPanel()
        {
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new FluentDropdownMenuContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = ScrollContainer = new UserTrackingScrollContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = flow = new SearchContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0, 5),
                        },
                    }
                },
            };
        }
    }
}
