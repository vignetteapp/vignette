// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Configuration;

namespace Vignette.Game.Graphics.Backgrounds
{
    public class UserBackgroundColour : UserBackground
    {
        private Bindable<Colour4> colour;

        public UserBackgroundColour()
        {
            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both,
            };
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);
            colour.BindValueChanged(e => this.FadeColour(e.NewValue, 200, Easing.OutQuint), true);
        }
    }
}
