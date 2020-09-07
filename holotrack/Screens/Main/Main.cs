using holotrack.Input;
using holotrack.Overlays.Settings;
using holotrack.Overlays.SidePanel;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;

namespace holotrack.Screens.Main
{
    public class Main : Screen, IKeyBindingHandler<HoloTrackAction>
    {
        private SettingsOverlay settings = new SettingsOverlay();
        private SidePanelOverlay panel = new SidePanelOverlay();

        [BackgroundDependencyLoader]
        private void load(CubismAssetStore assets)
        {

            AddRangeInternal(new Drawable[]
            {
                new AdjustableBackground(),
                new AdjustableCubismSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Asset = assets.Get(@"haru_greeter.haru_greeter.model3.json"),
                },
                settings,
                panel.With(p =>
                {
                    p.Anchor = Anchor.BottomRight;
                    p.Origin = Anchor.BottomRight;
                }),
            });
        }

        public bool OnPressed(HoloTrackAction action)
        {
            switch (action)
            {
                case HoloTrackAction.ToggleSettings:
                    settings.ToggleVisibility();
                    return true;

                case HoloTrackAction.ToggleCamera:
                    panel.ToggleVisibility();
                    return true;
                
                default:
                    return false;
            }
        }

        public void OnReleased(HoloTrackAction action)
        {
        }
    }
}