using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace holotrack.Core.Screens.Main
{
    public class AdjustableCubismSprite : CubismSprite
    {
        public bool Adjustable;
        public float ScrollSpeed = 5;
        public double MinScale = 0.1;
        public double MaxScale = 3;

        protected override bool OnDragStart(DragStartEvent e) => true;

        protected override void OnDrag(DragEvent e)
        {
            base.OnDrag(e);

            if (!Adjustable || e.Button == MouseButton.Right) return;

            Renderer.Translation += e.Delta * 2;
            Invalidate(Invalidation.DrawNode);
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            if (Adjustable)
            {
                Renderer.Scale += new Vector2(e.ScrollDelta.Y / ScrollSpeed);
                Renderer.Scale.X = (float)MathHelper.Clamp(Renderer.Scale.X, MinScale, MaxScale);
                Renderer.Scale.Y = (float)MathHelper.Clamp(Renderer.Scale.Y, MinScale, MaxScale);
                Invalidate(Invalidation.DrawNode);
            }

            return base.OnScroll(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;
    }
}