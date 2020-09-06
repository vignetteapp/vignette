using System.Collections.Generic;
using osu.Framework.Input.Bindings;

namespace holotrack.Input
{
    public class HoloTrackKeyBindingContainer : KeyBindingContainer<HoloTrackAction>
    {
        public HoloTrackKeyBindingContainer(SimultaneousBindingMode simultaneousMode = SimultaneousBindingMode.None, KeyCombinationMatchingMode matchingMode = KeyCombinationMatchingMode.Modifiers)
            : base(simultaneousMode, matchingMode)
        {
        }

        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.F1 }, HoloTrackAction.ToggleSettings),
            new KeyBinding(new[] { InputKey.F2 }, HoloTrackAction.ToggleCamera),
        };
    }

    public enum HoloTrackAction
    {
        ToggleCamera,
        ToggleSettings,
    }
}