// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Settings
{
    public class SettingsSubPanel : SettingsPanel
    {
        protected override Container<Drawable> Content => flow;

        private FillFlowContainer flow;

        protected override Drawable CreateContent() => new FluentScrollContainer
        {
            RelativeSizeAxes = Axes.Both,
            Child = flow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 5),
            },
        };

        protected override void PopIn() => this.MoveToX(0, 300, Easing.OutCirc);

        protected override void PopOut() => this.MoveToX(-400, 300, Easing.InCirc);
    }
}
