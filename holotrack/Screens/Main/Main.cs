using holotrack.Input;
using holotrack.Overlays.Settings;
using holotrack.Overlays.SidePanel;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;

namespace holotrack.Screens.Main
{
    public class Main : Screen, IKeyBindingHandler<HoloTrackAction>
    {
        private SettingsOverlay settings = new SettingsOverlay();

        [Cached]
        private SidePanelOverlay sidePanel = new SidePanelOverlay();

        [Cached]
        private AdjustableCubismSprite model = new AdjustableCubismSprite();

        protected override void LoadComplete()
        {
            AddRangeInternal(new Drawable[]
            {
                new AdjustableBackground(),
                model,
                settings,
                sidePanel
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
                    sidePanel.ToggleVisibility();
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