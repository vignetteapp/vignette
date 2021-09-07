// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Configuration;

namespace Vignette.Game.Screens.Stage
{
    public class SolidBackground : CompositeDrawable
    {
        private Box box;
        private Bindable<Colour4> colour;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            InternalChild = box = new Box
            {
                RelativeSizeAxes = Axes.Both,
            };

            colour.BindValueChanged(e => box.Colour = e.NewValue, true);
        }
    }
}
