// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A checkbox where its state can be toggled.
    /// </summary>
    public class FluentCheckbox : Checkbox, IHasText
    {
        private readonly ThemableEffectBox box;

        private readonly ThemableSpriteIcon check;

        private readonly FillFlowContainer flow;

        private ThemableSpriteText text;

        /// <summary>
        /// Gets or sets the text label for this checkbox.
        /// </summary>
        public LocalisableString Text
        {
            get => text?.Text ?? default;
            set
            {
                if (text == null)
                {
                    flow.Add(text = new ThemableSpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                text.Text = value;
            }
        }

        public FluentCheckbox()
        {
            Height = 20;
            AutoSizeAxes = Axes.X;
            Child = flow = new FillFlowContainer
            {
                Direction = FillDirection.Horizontal,
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,
                Spacing = new Vector2(5, 0),
                Child = new Container
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
                            Icon = FluentSystemIcons.Checkmark16,
                            Size = new Vector2(12),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        },
                    },
                },
            };

            Current.BindDisabledChanged(_ => Scheduler.AddOnce(updateState), true);
        }

        private void updateState()
        {
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

                box.BorderColour = !Current.Disabled
                    ? ThemeSlot.Gray160
                    : ThemeSlot.Gray90;

                box.Colour = ThemeSlot.White;
                box.BorderThickness = 1.5f;
            }

            if (text != null)
                text.Colour = !Current.Disabled ? ThemeSlot.Gray190 : ThemeSlot.Gray90;
        }

        protected override void OnUserChange(bool value)
            => updateState();

        protected override bool OnHover(HoverEvent e)
        {
            if (Current.Disabled)
                return false;

            updateState();
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            updateState();
            base.OnHoverLost(e);
        }
    }
}
