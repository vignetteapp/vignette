// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;

namespace Vignette.Game.Settings
{
    public abstract class SettingsPanel : VisibilityContainer
    {
        protected ThemableBox Background { get; private set; }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                Background = new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new FluentDropdownMenuContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = CreateContent(),
                },
            };
        }

        protected abstract Drawable CreateContent();
    }
}
