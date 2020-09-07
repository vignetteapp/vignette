using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace holotrack.Overlays.SidePanel
{
    public class SidePanelOverlay : FocusedOverlayContainer
    {
        private const float panel_padding = 10;

        public SidePanelOverlay()
        {
            AutoSizeAxes = Axes.X;
            RelativeSizeAxes = Axes.Y;
            Margin = new MarginPadding(panel_padding);

            Child = new GridContainer
            {
                AutoSizeAxes = Axes.X,
                RelativeSizeAxes = Axes.Y,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.Distributed),
                    new Dimension(GridSizeMode.AutoSize),
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                    },
                    new Drawable[]
                    {
                        new CameraDisplay
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                        },
                    }
                }
            };
        }

        protected override void PopIn() => this.MoveToX(0, 200, Easing.OutQuint);
        protected override void PopOut() => this.MoveToX(DrawSize.X + (panel_padding * 2), 200, Easing.OutQuint);
    }
}