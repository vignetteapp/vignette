// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class Checkbox : osu.Framework.Graphics.UserInterface.Checkbox
    {
        private readonly Container stroke;

        private readonly Box accentBox;

        private readonly SpriteIcon check;

        private Bindable<Theme> theme;

        public Checkbox()
        {
            Size = new Vector2(40);
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
                check.Colour = e.NewValue.White;
                stroke.BorderColour = e.NewValue.White;
            }, true);
        }

        protected override void OnUserChange(bool value)
        {
            check.Alpha = accentBox.Alpha = value ? 1 : 0;
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (!Current.Value)
                check.Alpha = 1;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (!Current.Value)
                check.Alpha = 0;
        }
    }
}
