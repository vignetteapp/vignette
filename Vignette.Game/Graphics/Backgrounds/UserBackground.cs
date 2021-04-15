// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Configuration;

namespace Vignette.Game.Graphics.Backgrounds
{
    public abstract class UserBackground : CompositeDrawable
    {
        private Bindable<Vector2> offset;

        private Bindable<float> scale;

        private Bindable<float> rotation;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            offset = config.GetBindable<Vector2>(VignetteSetting.BackgroundOffset);
            offset.ValueChanged += e => this.MoveTo(e.NewValue, 200, Easing.OutQuint);

            scale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
            scale.ValueChanged += e => this.ScaleTo(e.NewValue, 200, Easing.OutQuint);

            rotation = config.GetBindable<float>(VignetteSetting.BackgroundRotation);
            rotation.ValueChanged += e => this.RotateTo(e.NewValue, 200, Easing.OutQuint);

            this.MoveTo(offset.Value);
            this.ScaleTo(scale.Value);
            this.RotateTo(rotation.Value);
        }
    }
}
