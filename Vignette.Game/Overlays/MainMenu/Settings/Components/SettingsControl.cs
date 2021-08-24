// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public abstract class SettingsControl : SettingsItem
    {
        protected abstract Drawable CreateControl();

        protected Drawable Control { get; }

        public SettingsControl()
        {
            Add(Control = CreateControl());
        }
    }

    public abstract class SettingsControl<T> : SettingsControl, IHasCurrentValue<T>
    {
        public Bindable<T> Current
        {
            get => controlWithCurrent.Current;
            set => controlWithCurrent.Current = value;
        }

        private IHasCurrentValue<T> controlWithCurrent => Control as IHasCurrentValue<T>;
    }
}
