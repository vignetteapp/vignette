// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Configuration;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Game.Screens.Stage
{
    public class Avatar : CompositeDrawable
    {
        private CubismModel model;
        private Bindable<bool> adjustable;
        private Bindable<Vector2> offset;
        private Bindable<float> scale;
        private Bindable<float> rotation;
        private bool shouldEase;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, VignetteGameBase game, SessionConfigManager session)
        {
            RelativeSizeAxes = Axes.Both;

            adjustable = session.GetBindable<bool>(SessionSetting.EditingAvatar);

            offset = config.GetBindable<Vector2>(VignetteSetting.AvatarOffset);
            offset.ValueChanged += _ => handleVisualChange();

            rotation = config.GetBindable<float>(VignetteSetting.AvatarRotation);
            rotation.ValueChanged += _ => handleVisualChange();

            scale = config.GetBindable<float>(VignetteSetting.AvatarScale);
            scale.ValueChanged += _ => handleVisualChange();

            AddInternal(model = new CubismModel(new NamespacedResourceStore<byte[]>(game.Resources, "Model"))
            {
                Size = new Vector2(1024),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new CubismController[]
                {
                    new AvatarController(),
                    new CubismPhysicsController(),
                    new CubismBreathController(
                        new CubismBreathParameter("ParamAngleX", 0.0f, 15.0f, 6.5345f, 0.5f),
                        new CubismBreathParameter("ParamAngleY", 0.0f, 8.0f, 3.5345f, 0.5f),
                        new CubismBreathParameter("ParamAngleZ", 0.0f, 10.0f, 5.5345f, 0.5f),
                        new CubismBreathParameter("ParamBodyAngleX", 0.0f, 4.0f, 15.5345f, 0.5f),
                        new CubismBreathParameter("ParamBreath", 0.5f, 0.5f, 3.2345f, 0.5f)
                    ),
                }
            });
        }

        private void handleVisualChange()
        {
            model?.MoveTo(offset.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            model?.RotateTo(rotation.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            model?.ResizeTo(512 * scale.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            shouldEase = true;
        }

        protected override bool OnDragStart(DragStartEvent e)
        {
            if (adjustable.Value)
                return true;

            return base.OnDragStart(e);
        }

        protected override void OnDrag(DragEvent e)
        {
            base.OnDrag(e);
            offset.Value += e.Delta;
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            if (adjustable.Value)
            {
                scale.Value += e.ScrollDelta.Y * 0.1f;
                return true;
            }

            return base.OnScroll(e);
        }
    }
}
