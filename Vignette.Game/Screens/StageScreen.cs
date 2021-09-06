// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using System;
using Vignette.Game.Configuration;

namespace Vignette.Game.Screens
{
    public class StageScreen : VignetteScreen, IHasContextMenu
    {
        private Box background;
        private Bindable<Colour4> colour;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            AddRangeInternal(new Drawable[]
            {
                background = new Box { RelativeSizeAxes = Axes.Both },
            });

            colour.BindValueChanged(e => background.Colour = e.NewValue, true);
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeOut().Delay(500).FadeInFromZero(500, Easing.OutQuint);
        }

        public MenuItem[] ContextMenuItems => Array.Empty<MenuItem>();
    }
}
