// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Game.Settings
{
    public class StatefulSettingsSubSection<T> : SettingsSubSection, IHasCurrentValue<T>
    {
        public Bindable<T> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        /// <summary>
        /// A map to the possible states and the drawables representing those states. The key is the state
        /// and the value is the drawable representation of that state. Null values are accepted and means nothing to be shown.
        /// </summary>
        public Dictionary<T, Drawable> Map { get; set; }

        private readonly BindableWithCurrent<T> current = new BindableWithCurrent<T>();

        protected override void LoadComplete()
        {
            if (Map == null)
                throw new InvalidOperationException($"{nameof(Map)} must be defined.");

            base.LoadComplete();

            Current.BindValueChanged(handleStateChange, true);
        }

        private void handleStateChange(ValueChangedEvent<T> e)
        {
            Clear(false);

            var next = Map[e.NewValue];

            if (next != null)
                Add(next);
        }
    }
}
