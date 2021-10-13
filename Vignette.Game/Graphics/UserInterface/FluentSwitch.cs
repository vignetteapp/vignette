// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentSwitch : FluentCheckboxBase
    {
        private ThemableCircle background;
        private ThemableCircle nub;
        private Container nubContainer;

        protected override Drawable CreateCheckbox() => new Container
        {
            Size = new Vector2(40, 20),
            Children = new Drawable[]
            {
                background = new ThemableCircle
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Vertical = 5, Left = 5, Right = 15 },
                    Child = nubContainer = new Container
                    {
                        Size = new Vector2(10),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        RelativePositionAxes = Axes.X,
                        Child = nub = new ThemableCircle
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = ThemeSlot.Black,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        }
                    }
                }
            }
        };

        protected override void UpdateState()
        {
            base.UpdateState();

            if (!Current.Disabled)
                nubContainer.ScaleTo(IsHovered ? 1.2f : 1.0f, 500, Easing.OutQuint);

            if (Current.Value)
            {
                nubContainer.MoveToX(1, 500, Easing.OutQuint);

                if (!Current.Disabled)
                {
                    background.Colour = ThemeSlot.AccentPrimary;
                }
                else
                {
                    background.Colour = ThemeSlot.Gray60;
                    nub.Colour = ThemeSlot.White;
                }

                background.BorderThickness = 0.0f;
            }
            else
            {
                nubContainer.MoveToX(0, 500, Easing.OutQuint);

                background.BorderColour = nub.Colour = !Current.Disabled ? ThemeSlot.Gray160 : ThemeSlot.Gray60;
                background.Colour = ThemeSlot.Gray20;
                background.BorderThickness = 1.5f;
            }
        }
    }
}
