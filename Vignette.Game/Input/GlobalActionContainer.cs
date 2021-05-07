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
        private InputManager parentInputManager;

        public GlobalActionContainer(VignetteGameBase game)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<GlobalAction>)
                handler = game;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            parentInputManager = GetContainingInputManager();
        }

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
        };
    }

    public enum GlobalAction
    {
        [Description("Open/Close Main Menu")]
        ToggleMainMenu,
    }
}
