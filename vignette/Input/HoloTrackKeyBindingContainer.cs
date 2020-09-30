using System.Collections.Generic;
using osu.Framework.Input.Bindings;

namespace vignette.Input
{
    public class VignetteKeyBindingContainer : KeyBindingContainer<VignetteAction>
    {
        public VignetteKeyBindingContainer(SimultaneousBindingMode simultaneousMode = SimultaneousBindingMode.None, KeyCombinationMatchingMode matchingMode = KeyCombinationMatchingMode.Modifiers)
            : base(simultaneousMode, matchingMode)
        {
        }

        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.F1 }, VignetteAction.ToggleSettings),
            new KeyBinding(new[] { InputKey.F2 }, VignetteAction.ToggleCamera),
        };
    }

    public enum VignetteAction
    {
        ToggleCamera,
        ToggleSettings,
    }
}