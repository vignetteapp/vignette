// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public abstract class ThemedButton : Button
    {
        private Drawable label;

        private readonly ThemedSolidBox overlay;

        private readonly ThemedSolidBox background;

        private ButtonStyle style;

        public ButtonStyle Style
        {
            get => style;
            set
            {
                if (style == value)
                    return;

                style = value;
                UpdateStyle();
            }
        }

        private ThemeColour labelColour;

        public ThemeColour LabelColour
        {
            get => labelColour;
            set
            {
                if (labelColour == value)
                    return;

                labelColour = value;
                UpdateStyle();
            }
        }

        public ThemedButton()
        {
            Masking = true;
            CornerRadius = 2.5f;

            AddRange(new Drawable[]
            {
                background = new ThemedSolidBox
                {
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.ThemePrimary,
                },
                overlay = new ThemedSolidBox
                {
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.Black,
                    Alpha = 0.0f,
                },
                label = CreateLabel(),
            });

            UpdateStyle();
            Enabled.BindValueChanged(state => Colour = state.NewValue ? Colour4.White : Colour4.Gray, true);
        }

        protected void UpdateStyle()
        {
            background.Alpha = Style == ButtonStyle.Filled ? 1.0f : 0.0f;

            if (label is not IThemeable themeable)
                return;

            switch (Style)
            {
                case ButtonStyle.Filled:
                    themeable.ThemeColour = ThemeColour.NeutralPrimary;
                    break;

                case ButtonStyle.NoFill:
                    themeable.ThemeColour = ThemeColour.ThemePrimary;
                    break;

                case ButtonStyle.Override:
                    themeable.ThemeColour = LabelColour;
                    break;
            }
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

        Override,
    }
}
