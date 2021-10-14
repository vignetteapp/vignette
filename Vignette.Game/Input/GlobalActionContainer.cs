// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;

namespace Vignette.Game.Input
{
    public class GlobalActionContainer : KeyBindingContainer<GlobalAction>, IHandleGlobalKeyboardInput
    {
        private readonly Drawable handler;
        private readonly VignetteKeyBindManager keybindManager;
        private InputManager parentInputManager;

        protected override IEnumerable<Drawable> KeyBindingInputQueue
        {
            get
            {
                var queue = parentInputManager?.NonPositionalInputQueue ?? base.KeyBindingInputQueue;
                return handler != null ? queue.Prepend(handler) : queue;
            }
        }

        public override IEnumerable<IKeyBinding> DefaultKeyBindings => GlobalKeyBindings;

        public GlobalActionContainer(VignetteGameBase game, VignetteKeyBindManager keybindManager = null)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<GlobalAction>)
                handler = game;

            if (keybindManager != null)
            {
                this.keybindManager = keybindManager;
                this.keybindManager.KeyBindsChanged += ReloadMappings;
            }
        }

        protected override void LoadComplete()
        {
            parentInputManager = GetContainingInputManager();
            base.LoadComplete();
        }

        protected override void ReloadMappings()
        {
            KeyBindings = keybindManager?.Global ?? DefaultKeyBindings;
        }

        public static IEnumerable<IKeyBinding> GlobalKeyBindings => new[]
        {
            new KeyBinding(InputKey.Escape, GlobalAction.ToggleSettings),
            new KeyBinding(InputKey.F12, GlobalAction.TakeScreenshot),
        };
    }

    public enum GlobalAction
    {
        [Description("Toggle Settings")]
        ToggleSettings,

        [Description("Take Screenshot")]
        TakeScreenshot,
    }
}
