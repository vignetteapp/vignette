// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsSlider<T> : SettingsExpandedControl<FluentSlider<T>, T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private ThemableSpriteText text;

        protected override FluentSlider<T> CreateControl() => new FluentSlider<T> { RelativeSizeAxes = Axes.X };

        protected override void LoadComplete()
        {
            base.LoadComplete();

            LabelContainer.Add(text = new ThemableSpriteText
            {
                Font = SegoeUI.SemiBold.With(size: 16),
                Colour = ThemeSlot.Black,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
            });

            Current.BindValueChanged(e => text.Text = $"{e.NewValue:0.##}", true);
        }
    }
}
