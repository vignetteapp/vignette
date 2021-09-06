// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class FluentCheckboxBase : Checkbox, IHasText
    {
        private readonly Drawable checkbox;
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
                    Remove(checkbox);

                    Add(new FillFlowContainer
                    {
                        Direction = FillDirection.Horizontal,
                        RelativeSizeAxes = Axes.Y,
                        AutoSizeAxes = Axes.X,
                        Spacing = new Vector2(5, 0),
                        Children = new Drawable[]
                        {
                            checkbox,
                            text = new ThemableSpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                            },
                        },
                    });
                }

                text.Text = value;
            }
        }

        public FluentCheckboxBase()
        {
            Height = 20;
            AutoSizeAxes = Axes.X;
            Child = checkbox = CreateCheckbox();

            Current.BindDisabledChanged(_ => Scheduler.AddOnce(UpdateState), true);
        }

        protected virtual void UpdateState()
        {
            if (text != null)
                text.Colour = !Current.Disabled ? ThemeSlot.Gray190 : ThemeSlot.Gray90;
        }

        protected abstract Drawable CreateCheckbox();

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => checkbox.ReceivePositionalInputAt(screenSpacePos);

        protected override void OnUserChange(bool value) => UpdateState();

        protected override bool OnHover(HoverEvent e)
        {
            if (Current.Disabled)
                return false;

            UpdateState();
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            UpdateState();
            base.OnHoverLost(e);
        }
    }
}
