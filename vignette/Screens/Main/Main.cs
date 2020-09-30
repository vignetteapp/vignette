using vignette.Input;
using vignette.Overlays.Settings;
using vignette.Overlays.SidePanel;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;

namespace vignette.Screens.Main
{
    public class Main : Screen, IKeyBindingHandler<VignetteAction>
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

        public bool OnPressed(VignetteAction action)
        {
            switch (action)
            {
                case VignetteAction.ToggleSettings:
                    settings.ToggleVisibility();
                    return true;

                case VignetteAction.ToggleCamera:
                    sidePanel.ToggleVisibility();
                    return true;
                
                default:
                    return false;
            }
        }

        public void OnReleased(VignetteAction action)
        {
        }
    }
}