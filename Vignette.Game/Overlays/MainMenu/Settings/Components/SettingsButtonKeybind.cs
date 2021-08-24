// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsButtonKeybind : SettingsButton
    {
        public readonly IKeyBinding TargetAction;

        public SettingsButtonKeybind(IKeyBinding keyBind)
        {
            TargetAction = keyBind;

            Label = keyBind.Action.GetDescription();
            Control.Text = keyBind.KeyCombination.ReadableString();
        }

        protected override Drawable CreateControl() => new FluentButton
        {
            Width = 200,
            Style = ButtonStyle.Text,
        };
    }
}
