// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public abstract class VignetteButton : Button
    {
        private Drawable label;

        private readonly VignetteBox overlay;

        private readonly VignetteBox background;

        private ButtonStyle style;

        public ButtonStyle Style
        {
            get => style;
            set
            {
                if (style == value)
                    return;

                style = value;
                updateStyle();
            }
        }

        public VignetteButton()
        {
            Masking = true;
            CornerRadius = VignetteStyle.CornerRadius;

            AddRange(new Drawable[]
            {
                background = new VignetteBox
                {
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.ThemePrimary,
                },
                overlay = new VignetteBox
                {
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.Black,
                    Alpha = 0.0f,
                },
                label = CreateLabel(),
            });

            updateStyle();
            Enabled.BindValueChanged(state => Colour = state.NewValue ? Colour4.White : Colour4.Gray, true);
        }

        private void updateStyle()
        {
            bool isFilled = Style == ButtonStyle.Filled;

            background.Alpha = isFilled ? 1.0f : 0.0f;
            if (label is IThemeable themeable)
                themeable.ThemeColour = isFilled ? ThemeColour.White : ThemeColour.ThemePrimary;
        }

        protected abstract Drawable CreateLabel();

        protected override bool OnHover(HoverEvent e)
        {
            if (Enabled.Value)
                overlay.Alpha = 0.05f;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (Enabled.Value && !e.HasAnyButtonPressed)
                overlay.Alpha = 0.0f;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (Enabled.Value)
                overlay.Alpha = 0.1f;

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (Enabled.Value)
                overlay.Alpha = ScreenSpaceDrawQuad.Contains(e.ScreenSpaceMousePosition) ? 0.05f : 0.0f;

            base.OnMouseUp(e);
        }
    }

    public enum ButtonStyle
    {
        Filled,

        NoFill,
    }
}
