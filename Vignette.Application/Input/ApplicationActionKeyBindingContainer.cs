// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Input.Bindings;

namespace Vignette.Application.Input
{
    public class ApplicationActionKeyBindingContainer : KeyBindingContainer<ApplicationAction>
    {
        public override IEnumerable<IKeyBinding> DefaultKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.Escape }, ApplicationAction.ToggleNavigation),
        };
    }

    public enum ApplicationAction
    {
        ToggleNavigation,
    }
}
