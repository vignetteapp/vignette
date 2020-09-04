using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;

namespace holotrack.Overlays
{
    public abstract class HoloTrackFocusedOverlayContainer : FocusedOverlayContainer
    {
        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => true;
        protected override void PopIn() => this.FadeIn(200, Easing.OutQuint).ScaleTo(1.0f, 200, Easing.OutQuint);
        protected override void PopOut() => this.FadeOut(200, Easing.OutQuint).ScaleTo(0.75f, 200, Easing.OutQuint);

        protected override bool OnClick(ClickEvent e)
        {
            if (!base.ReceivePositionalInputAt(e.ScreenSpaceMouseDownPosition))
                Hide();

            return base.OnClick(e);
        }
    }
}