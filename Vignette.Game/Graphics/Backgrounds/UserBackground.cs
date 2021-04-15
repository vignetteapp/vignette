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
        private bool affectPosition;

        private bool affectColour;

        private Bindable<Vector2> offset;

        private Bindable<float> scale;

        private Bindable<float> rotation;

        private Bindable<Colour4> colour;

        private Bindable<BackgroundColourBlending> blending;

        public UserBackground(bool affectPosition = true, bool affectColour = true)
        {
            this.affectColour = affectColour;
            this.affectPosition = affectPosition;
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            if (affectPosition)
            {
                offset = config.GetBindable<Vector2>(VignetteSetting.BackgroundOffset);
                offset.ValueChanged += e => this.MoveTo(e.NewValue, 200, Easing.OutQuint);

                scale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
                scale.ValueChanged += e => this.ScaleTo(e.NewValue, 200, Easing.OutQuint);

                rotation = config.GetBindable<float>(VignetteSetting.BackgroundRotation);
                rotation.ValueChanged += e => this.RotateTo(e.NewValue, 200, Easing.OutQuint);

                Position = offset.Value;
                Rotation = rotation.Value;
                Scale = new Vector2(scale.Value);
            }

            if (affectColour)
            {
                colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);
                colour.ValueChanged += e => this.FadeColour(e.NewValue, 200, Easing.OutQuint);

                blending = config.GetBindable<BackgroundColourBlending>(VignetteSetting.BackgroundColourBlending);
                blending.ValueChanged += e => Blending = getBlendingParameters(e.NewValue);

                Colour = colour.Value;
                Blending = getBlendingParameters(blending.Value);
            }
        }

        private static BlendingParameters getBlendingParameters(BackgroundColourBlending blend)
        {
            switch (blend)
            {
                case BackgroundColourBlending.Inherit:
                default:
                    return BlendingParameters.Inherit;

                case BackgroundColourBlending.Mixture:
                    return BlendingParameters.Mixture;

                case BackgroundColourBlending.Additive:
                    return BlendingParameters.Additive;
            }
        }
    }
}
