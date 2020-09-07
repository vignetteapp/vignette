using holotrack.Configuration;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace holotrack.Screens.Main
{
    public class AdjustableCubismSprite : CubismSprite
    {
        private Bindable<float> userScale;
        private Bindable<float> userXOffset;
        private Bindable<float> userYOffset;
        private Bindable<bool> allowMouseDrag;
        private Bindable<bool> allowMouseScroll;

        public float ScrollSpeed = 5;
        public double MinScale = 0.1;
        public double MaxScale = 3;

        [BackgroundDependencyLoader]
        private void load(HoloTrackConfigManager config)
        {
            userScale = config.GetBindable<float>(HoloTrackSetting.ModelScale);
            userXOffset = config.GetBindable<float>(HoloTrackSetting.ModelPositionX);
            userYOffset = config.GetBindable<float>(HoloTrackSetting.ModelPositionY);
            
            allowMouseDrag = config.GetBindable<bool>(HoloTrackSetting.MouseDrag);
            allowMouseScroll = config.GetBindable<bool>(HoloTrackSetting.MouseWheel);

            userScale.ValueChanged += _ => updateRenderer();
            userXOffset.ValueChanged += _ => updateRenderer();
            userYOffset.ValueChanged += _ => updateRenderer();

            updateRenderer();
        }

        private void updateRenderer()
        {
            Renderer.Translation = new Vector2(userXOffset.Value, userYOffset.Value);
            Renderer.Scale = new Vector2(userScale.Value);
            Invalidate(Invalidation.DrawNode);
        }

        protected override bool OnDragStart(DragStartEvent e) => true;

        protected override void OnDrag(DragEvent e)
        {
            base.OnDrag(e);

            if (!allowMouseDrag.Value || (e.Button == MouseButton.Right)) return;

            userXOffset.Value += e.Delta.X * 2;
            userYOffset.Value += e.Delta.Y * 2;
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            if (allowMouseScroll.Value)
                userScale.Value += e.ScrollDelta.Y / ScrollSpeed;

            return base.OnScroll(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;
    }
}