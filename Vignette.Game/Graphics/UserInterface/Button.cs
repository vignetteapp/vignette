// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using Vignette.Game.Graphics.Themes;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class Button : osu.Framework.Graphics.UserInterface.Button
    {
        protected abstract Drawable CreateLabel();

        private Colour4 disabledColour = Colour4.Black;

        protected Colour4 DisabledColour
        {
            get => disabledColour;
            set
            {
                if (disabledColour.Equals(value))
                    return;

                disabledColour = value;
                theme?.TriggerChange();
            }
        }

        private bool isFilled;

        public bool IsFilled
        {
            get => isFilled;
            set
            {
                if (isFilled == value)
                    return;

                isFilled = value;
                theme?.TriggerChange();
            }
        }

        protected readonly Drawable Label;

        private readonly Box background;

        private readonly Box overlay;

        public Button()
        {
            Height = 40;
            Masking = true;
            CornerRadius = 5;
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                overlay = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                },
                Label = CreateLabel(),
            };

            Enabled.BindValueChanged(_ => theme?.TriggerChange(), true);
        }

        private Bindable<Theme> theme;

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                background.Colour = IsFilled ? e.NewValue.AccentPrimary : Colour4.Transparent;
                overlay.Colour = e.NewValue.Black;
                DisabledColour = e.NewValue.NeutralSecondary;
                Label.Colour = e.NewValue.White;
            }, true);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (Enabled.Value)
                overlay.Alpha = 0.1f;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (Enabled.Value && !e.HasAnyButtonPressed)
                overlay.Alpha = 0;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (Enabled.Value)
                overlay.Alpha = 0.2f;

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            if (Enabled.Value)
                overlay.Alpha = ScreenSpaceDrawQuad.Contains(e.ScreenSpaceMousePosition) ? 0.1f : 0;
        }
    }
}
