// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Input.Bindings;

namespace Vignette.Game.Configuration.Converters
{
    public class StringKeyBindingConverter : KeybindingConverter<string>
    {
        protected override string StringifyAction(IKeyBinding keybind) => keybind.Action.ToString();

        protected override bool TryGetAction(string name, out string value)
        {
            value = name;
            return true;
        }
    }
}
