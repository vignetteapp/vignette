// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Configuration;
using Vignette.Game.IO;
using Vignette.Game.Tracking;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Game.Screens.Stage
{
    public class Avatar : CompositeDrawable
    {
        private CubismModel model;
        private Bindable<bool> adjustable;
        private Bindable<string> path;
        private Bindable<Vector2> offset;
        private Bindable<float> scale;
        private Bindable<float> rotation;
        private bool shouldEase;

        [Resolved]
        private GameHost host { get; set; }

        [Cached]
        private TrackingComponent tracker = new TrackingComponent();

        [Cached]
        private MotionController controller = new MotionController();

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, SessionConfigManager session)
        {
            RelativeSizeAxes = Axes.Both;

            adjustable = session.GetBindable<bool>(SessionSetting.EditingAvatar);

            offset = config.GetBindable<Vector2>(VignetteSetting.AvatarOffset);
            offset.ValueChanged += _ => handleVisualChange();

            rotation = config.GetBindable<float>(VignetteSetting.AvatarRotation);
            rotation.ValueChanged += _ => handleVisualChange();

            scale = config.GetBindable<float>(VignetteSetting.AvatarScale);
            scale.ValueChanged += _ => handleVisualChange();

            path = config.GetBindable<string>(VignetteSetting.AvatarPath);
            path.BindValueChanged(_ => handlePathChange(), true);

            AddInternal(tracker);

            if (model != null)
            {
                model.Add(controller);
            }
        }

        private void handleVisualChange()
        {
            model?.MoveTo(offset.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            model?.RotateTo(rotation.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            model?.ResizeTo(512 * scale.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            shouldEase = true;
        }

        private void handlePathChange()
        {
            shouldEase = false;

            tracker.Dispose();

            model?.Expire();

            if (!string.IsNullOrEmpty(path.Value) && tryCreateCubismModel(path.Value, out var newModel))
            {
                AddInternal(model = newModel.With(m =>
                {
                    m.Size = new Vector2(512);
                    m.Anchor = Anchor.Centre;
                    m.Origin = Anchor.Centre;
                }));
            }

            tracker = new TrackingComponent();

            handleVisualChange();
        }

        private bool tryCreateCubismModel(string path, out CubismModel model)
        {
            try
            {
                model = new CubismModelAvatar(new RecursiveNativeStorage(path, host));
                return true;
            }
            catch (FileNotFoundException)
            {
                model = null;
                return false;
            }
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
