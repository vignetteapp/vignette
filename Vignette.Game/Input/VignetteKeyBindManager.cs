// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Framework.Input.Bindings;
using osu.Framework.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using Vignette.Game.Configuration;

namespace Vignette.Game.Input
{
    public class VignetteKeyBindManager : JsonConfigManager
    {
        protected override string Filename => "keybinds.json";

        [JsonIgnore]
        public Action KeyBindsChanged;

        [JsonProperty]
        private readonly BindableDictionary<string, string> global = new BindableDictionary<string, string>();

        [JsonIgnore]
        public IEnumerable<IKeyBinding> Global
        {
            get
            {
                foreach ((string key, string value) in global)
                {
                    if (!Enum.TryParse<GlobalAction>(key, out var action))
                        continue;

                    var keyStrings = value.Split('+', StringSplitOptions.RemoveEmptyEntries);
                    var keys = new List<InputKey>();

                    foreach (var keyString in keyStrings)
                    {
                        if (Enum.TryParse<InputKey>(keyString, out var inputKey))
                            keys.Add(inputKey);
                    }

                    if (keys.Count > 0)
                        yield return new KeyBinding(keys.ToArray(), action);
                }
            }
            set
            {
                foreach (var keybind in value)
                {
                    string sKey = keybind.GetAction<GlobalAction>().ToString();
                    string sValue = string.Join('+', keybind.KeyCombination.Keys.Select(k => k.ToString()));
                    global[sKey] = sValue;
                }

                PerformSave();
            }
        }

        public VignetteKeyBindManager(Storage storage)
            : base(storage)
        {
        }

        public void SetKeyBind<T>(T action, params InputKey[] keys)
            where T : struct
        {
            if (keys.Length == 0)
                return;

            if (action is GlobalAction)
                global[action.ToString()] = string.Join('+', keys);

            PerformSave();
        }

        protected override bool PerformSave()
        {
            KeyBindsChanged?.Invoke();
            return base.PerformSave();
        }
    }
}
