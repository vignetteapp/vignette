// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Configuration;

namespace Vignette.Game.Screens.Stage
{
    public class StageBackground : CompositeDrawable
    {
        private CompositeDrawable background;
        private Bindable<BackgroundType> type;
        private Bindable<Vector2> position;
        private Bindable<float> rotation;
        private Bindable<float> scale;
        private Bindable<bool> adjustable;
        private bool shouldEase;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config, SessionConfigManager session)
        {
            RelativeSizeAxes = Axes.Both;

            position = config.GetBindable<Vector2>(VignetteSetting.BackgroundPosition);
            position.ValueChanged += _ => handleVisualChange();

            rotation = config.GetBindable<float>(VignetteSetting.BackgroundRotation);
            rotation.ValueChanged += _ => handleVisualChange();

            scale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
            scale.ValueChanged += _ => handleVisualChange();

            type = config.GetBindable<BackgroundType>(VignetteSetting.BackgroundType);
            type.BindValueChanged(_ => handleTypeChange(), true);

            adjustable = session.GetBindable<bool>(SessionSetting.EditingBackground);
        }

        private void handleVisualChange()
        {
            background.MoveTo(position.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            background.RotateTo(rotation.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            background.ScaleTo(scale.Value, shouldEase ? 200 : 0, Easing.OutQuint);
            shouldEase = true;
        }

        private void handleTypeChange()
        {
            shouldEase = false;

            background?.Expire();

            switch (type.Value)
            {
                case BackgroundType.Colour:
                    background = new SolidBackground();
                    break;

                case BackgroundType.Image:
                    background = new TexturedBackground();
                    break;

                case BackgroundType.Video:
                    background = new VideoBackground();
                    break;
            }

            AddInternal(background.With(d =>
            {
                d.Anchor = Anchor.Centre;
                d.Origin = Anchor.Centre;
                d.RelativeSizeAxes = Axes.Both;
            }));

            background.FadeInFromZero(300, Easing.OutQuint);

            handleVisualChange();
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
            position.Value += e.Delta;
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
