using osu.Framework.Graphics.Cubism;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace holotrack.Core.Graphics
{
    public class AdjustableCubismSprite : CubismSprite
    {
        public bool Adjustable;

        protected override bool OnDragStart(DragStartEvent e) => true;

        public AdjustableCubismSprite()
        {
            Size = new Vector2(1024);
        }

        protected override void OnDrag(DragEvent e)
        {
            base.OnDrag(e);

            if (!Adjustable) return;

            if (e.Button == MouseButton.Right) return;

            Position += e.Delta;
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            if (Adjustable)
                Scale = new Vector2((float)MathHelper.Clamp(Scale.Y + (e.ScrollDelta.Y * 0.25f), 0.1, 5));

            return base.OnScroll(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;
    }
}