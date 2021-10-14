// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Settings
{
    public class SettingsMainPanel : SettingsPanel
    {
        public string SearchTerm
        {
            get => flow.SearchTerm;
            set => flow.SearchTerm = value;
        }

        public UserTrackingScrollContainer ScrollContainer { get; private set; }

        protected override Container<Drawable> Content => flow;

        private readonly SearchContainer flow = new SearchContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0, 5),
        };

        protected override Drawable CreateContent() => ScrollContainer = new UserTrackingScrollContainer
        {
            RelativeSizeAxes = Axes.Both,
            Child = flow,
        };

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Show();
        }

        protected override void PopIn() => Alpha = 1;

        protected override void PopOut() => Alpha = 0;
    }
}
