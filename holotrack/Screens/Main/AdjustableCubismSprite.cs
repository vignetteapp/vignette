using holotrack.Configuration;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace holotrack.Screens.Main
{
    public class AdjustableCubismSprite : Container
    {
        public CubismSprite Sprite { get; private set; }

        private Bindable<float> userScale;
        private Bindable<float> userXOffset;
        private Bindable<float> userYOffset;
        private Bindable<bool> allowMouseDrag;
        private Bindable<bool> allowMouseScroll;
        private Bindable<string> asset;

        [Resolved]
        private CubismAssetStore assets { get; set; }

        public const float scroll_speed = 5;

        public AdjustableCubismSprite()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(HoloTrackConfigManager config)
        {
            userScale = config.GetBindable<float>(HoloTrackSetting.ModelScale);
            userXOffset = config.GetBindable<float>(HoloTrackSetting.ModelPositionX);
            userYOffset = config.GetBindable<float>(HoloTrackSetting.ModelPositionY);
            
            allowMouseDrag = config.GetBindable<bool>(HoloTrackSetting.MouseDrag);
            allowMouseScroll = config.GetBindable<bool>(HoloTrackSetting.MouseWheel);

            asset = config.GetBindable<string>(HoloTrackSetting.Model);

            userScale.ValueChanged += _ => updateRenderer();
            userXOffset.ValueChanged += _ => updateRenderer();
            userYOffset.ValueChanged += _ => updateRenderer();
            
            asset.ValueChanged += _ => updateModel();

            updateModel();
        }

        private void updateRenderer()
        {
            Sprite.Renderer.Translation = new Vector2(userXOffset.Value, userYOffset.Value);
            Sprite.Renderer.Scale = new Vector2(userScale.Value);
            Sprite.Invalidate(Invalidation.DrawNode);
        }

        private void updateModel()
        {
            Clear(true);

            Child = Sprite = new CubismSprite
            {
                Asset = assets.Get(asset.Value),
                RelativeSizeAxes = Axes.Both,
            };

            updateRenderer();
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
                userScale.Value += e.ScrollDelta.Y / scroll_speed;

            return base.OnScroll(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;
    }
}