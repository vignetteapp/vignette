// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Application.Input
{
    /// <summary>
    /// A component that can be triggered and have its state reset after a set amount of time.
    /// </summary>
    public class TriggerableComponent : Component, IHasCurrentValue<bool>
    {
        private double lastInteractionTime;

        private readonly double triggerTime;

        private readonly BindableWithCurrent<bool> current = new BindableWithCurrent<bool>();

        public Bindable<bool> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        /// <summary>
        /// An additional condition to check.
        /// </summary>
        public Func<bool> Condition;

        /// <summary>
        /// Create a new tracking contianer.
        /// </summary>
        /// <param name="time">Duration in which before there is a trigger.</param>
        public TriggerableComponent(double time)
        {
            triggerTime = time;
        }

        public void Trigger()
        {
            lastInteractionTime = Clock.CurrentTime;
        }

        protected override void Update()
        {
            base.Update();
            Current.Value = ((Clock.CurrentTime - lastInteractionTime) > triggerTime) && (Condition?.Invoke() ?? true);
        }
    }
}
