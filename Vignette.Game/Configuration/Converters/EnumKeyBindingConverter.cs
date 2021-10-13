// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Input.Bindings;

namespace Vignette.Game.Configuration.Converters
{
    public class EnumKeyBindingConverter<T> : KeybindingConverter<T>
        where T : struct, Enum
    {
        protected override string StringifyAction(IKeyBinding keybind) => keybind.GetAction<T>().ToString();

        protected override bool TryGetAction(string name, out T value) => Enum.TryParse(name, out value);
    }
}
