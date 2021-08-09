// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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

        public override IEnumerable<IKeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(InputKey.Escape, GlobalAction.ToggleMainMenu),
            new KeyBinding(new[] { InputKey.Control, InputKey.Comma }, GlobalAction.OpenSettings),
        };

        public GlobalActionContainer(VignetteGameBase game, VignetteKeyBindManager keybindManager = null)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<GlobalAction>)
                handler = game;

            if (keybindManager != null)
            {
                // TODO: Populate missing default keybinds
                this.keybindManager = keybindManager;
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
    }

    public enum GlobalAction
    {
        [Description("Show/Hide the Main Menu")]
        ToggleMainMenu,

        [Description("Open Settings")]
        OpenSettings,
    }
}
