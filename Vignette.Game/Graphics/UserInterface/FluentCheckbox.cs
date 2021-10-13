// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A checkbox where its state can be toggled.
    /// </summary>
    public class FluentCheckbox : FluentCheckboxBase
    {
        private ThemableEffectBox box;
        private ThemableSpriteIcon check;

        protected override Drawable CreateCheckbox() => new Container
        {
            Size = new Vector2(20),
            Children = new Drawable[]
            {
                box = new ThemableEffectBox
                {
                    CornerRadius = 2.5f,
                    RelativeSizeAxes = Axes.Both,
                },
                check = new ThemableSpriteIcon
                {
                    Icon = SegoeFluent.Checkmark,
                    Size = new Vector2(12),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            },
        };

        protected override void UpdateState()
        {
            base.UpdateState();

            if (Current.Value)
            {
                if (!Current.Disabled)
                {
                    box.Colour = IsHovered
                        ? ThemeSlot.AccentDarkAlt
                        : ThemeSlot.AccentPrimary;
                }
                else
                {
                    box.Colour = ThemeSlot.Gray60;
                }

                check.Colour = ThemeSlot.White;
                box.BorderThickness = 0.0f;
            }
            else
            {
                check.Alpha = IsHovered ? 1 : 0;
                check.Colour = ThemeSlot.Gray130;

                box.BorderColour = !Current.Disabled ? ThemeSlot.Gray160 : ThemeSlot.Gray60;
                box.Colour = ThemeSlot.White;
                box.BorderThickness = 1.5f;
            }
        }
    }
}
