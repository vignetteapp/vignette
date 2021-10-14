// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Game.Settings
{
    public abstract class SettingsControl<TDrawable, TValue> : SettingsItem, IHasCurrentValue<TValue>
        where TDrawable : Drawable, IHasCurrentValue<TValue>
    {
        public Bindable<TValue> Current
        {
            get => Control.Current;
            set => Control.Current = value;
        }

        protected TDrawable Control { get; set; }

        public SettingsControl()
        {
            InitializeControl();
        }

        protected abstract TDrawable CreateControl();

        protected virtual void InitializeControl()
        {
            Foreground.Add(Control = CreateControl().With(d =>
            {
                d.Anchor = Anchor.CentreRight;
                d.Origin = Anchor.CentreRight;
            }));
        }
    }
}
