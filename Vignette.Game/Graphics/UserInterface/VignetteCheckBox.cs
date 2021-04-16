// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class VignetteCheckbox : Checkbox
    {
        private readonly Container stroke;

        private readonly Box accentBox;

        private readonly SpriteIcon check;

        private Bindable<Theme> theme;

        private Colour4 colourUnhovered;

        private Colour4 colourHovered;

        private Colour4 colourChecked;

        public VignetteCheckbox()
        {
            Size = new Vector2(20);
            Children = new Drawable[]
            {
                stroke = new Container
                {
                    Size = new Vector2(20),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    CornerRadius = 5,
                    BorderThickness = 2,
                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Transparent,
                    },
                },
                new Container
                {
                    Size = new Vector2(20),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    CornerRadius = 5,
                    Child = accentBox = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0,
                    },
                },
                check = new SpriteIcon
                {
                    Size = new Vector2(10),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                    Icon = FluentSystemIcons.Checkmark24,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                accentBox.Colour = e.NewValue.AccentPrimary;
                colourChecked = e.NewValue.White;
                colourHovered = check.Colour = e.NewValue.NeutralTertiary;
                colourUnhovered = stroke.BorderColour = e.NewValue.NeutralPrimary;
            }, true);
        }

        protected override void OnUserChange(bool value)
        {
            check.Alpha = 1;
            accentBox.Alpha = value ? 1 : 0;
            check.Colour = value ? colourChecked : colourHovered;
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!Current.Value)
            {
                check.Alpha = 1;
                stroke.BorderColour = colourHovered;
            }

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (!Current.Value)
            {
                check.Alpha = 0;
                stroke.BorderColour = colourUnhovered;
            }
        }
    }
}
