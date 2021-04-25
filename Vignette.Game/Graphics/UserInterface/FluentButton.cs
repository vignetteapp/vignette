// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
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
    /// A button where an action is performed when clicked on.
    /// </summary>
    public class FluentButton : Button, IHasText, IHasIcon, IHasTooltip
    {
        /// <summary>
        /// Gets or sets the tooltip text shown when hovered over.
        /// </summary>
        public virtual string TooltipText { get; set; }

        private readonly FillFlowContainer label;

        private readonly ThemableMaskedBox background;

        private ThemableSpriteText text;

        private ThemableSpriteIcon icon;

        private ButtonStyle style = ButtonStyle.Secondary;

        /// <summary>
        /// Gets or sets how this button is displayed. See <see cref="ButtonStyle"/> for options.
        /// </summary>
        public ButtonStyle Style
        {
            get => style;
            set
            {
                if (style == value)
                    return;

                style = value;
                Scheduler.AddOnce(updateStyle);
            }
        }

        /// <summary>
        /// Gets or sets the text label for this button.
        /// </summary>
        public LocalisableString Text
        {
            get => text?.Text ?? default;
            set
            {
                if (text == null)
                {
                    label.Insert(1, text = new ThemableSpriteText
                    {
                        Font = SegoeUI.SemiBold.With(size: 14),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    });
                }

                text.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text label's font.
        /// </summary>
        public FontUsage Font
        {
            get => text?.Font ?? default;
            set
            {
                if (text == null)
                    return;

                text.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets the icon label for this button.
        /// </summary>
        public IconUsage Icon
        {
            get => icon?.Icon ?? default;
            set
            {
                if (icon == null)
                {
                    label.Insert(0, icon = new ThemableSpriteIcon
                    {
                        Size = new Vector2(16),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    });
                }

                icon.Icon = value;
            }
        }

        /// <summary>
        /// Gets or sets the icon label's size.
        /// </summary>
        public float IconSize
        {
            get => icon?.Size.X ?? default;
            set
            {
                if (icon == null)
                    return;

                icon.Size = new Vector2(value);
            }
        }

        public FluentButton()
        {
            Height = 32;
            InternalChildren = new Drawable[]
            {
                background = new ThemableMaskedBox
                {
                    RelativeSizeAxes = Axes.Both,
                    BorderColour = ThemeSlot.Gray110,
                },
                label = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding(8),
                    Spacing = new Vector2(8, 0),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };

            Enabled.BindValueChanged(_ => Scheduler.AddOnce(updateState), true);
        }

        protected override void LoadComplete()
        {
            updateStyle();
            base.LoadComplete();
        }

        private bool isHovered;
        private bool isPressed;
        private ThemeSlot backgroundHovered;
        private ThemeSlot backgroundPressed;
        private ThemeSlot backgroundResting;
        private ThemeSlot backgroundDisabled;
        private ThemeSlot labelResting;
        private ThemeSlot labelDisabled;

        private void updateState()
        {
            if (Enabled.Value)
            {
                if (isPressed)
                    background.Colour = backgroundPressed;
                else if (IsHovered)
                    background.Colour = backgroundHovered;
                else
                    background.Colour = backgroundResting;

                if (text != null)
                    text.Colour = labelResting;

                if (icon != null)
                    icon.Colour = labelResting;
            }
            else
            {
                background.Colour = backgroundDisabled;

                if (text != null)
                    text.Colour = labelDisabled;

                if (icon != null)
                    icon.Colour = labelDisabled;
            }
        }

        private void updateStyle()
        {
            switch (Style)
            {
                case ButtonStyle.Primary:
                    labelResting = ThemeSlot.White;
                    backgroundResting = ThemeSlot.AccentPrimary;
                    backgroundHovered = ThemeSlot.AccentDarkAlt;
                    backgroundPressed = ThemeSlot.AccentDark;
                    break;

                case ButtonStyle.Text:
                case ButtonStyle.Secondary:
                    labelResting = ThemeSlot.Gray190;
                    backgroundResting = ThemeSlot.White;
                    backgroundHovered = ThemeSlot.Gray20;
                    backgroundPressed = ThemeSlot.Gray30;
                    break;
            }

            labelDisabled = ThemeSlot.Gray90;

            backgroundDisabled = Style != ButtonStyle.Text
                ? ThemeSlot.Gray20
                : ThemeSlot.Transparent;

            background.CornerRadius = Style != ButtonStyle.Text
                ? 2.5f
                : 0.0f;

            background.BorderThickness = Style != ButtonStyle.Secondary
                ? 0.0f
                : 1.5f;

            updateState();
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (isPressed)
                return false;

            isPressed = true;
            updateState();

            if (!Enabled.Value)
                return false;

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (!isPressed)
                return;

            isPressed = false;
            updateState();

            base.OnMouseUp(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (isHovered)
                return false;

            isHovered = true;
            updateState();

            if (!Enabled.Value)
                return false;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!isHovered)
                return;

            isHovered = false;
            updateState();

            base.OnHoverLost(e);
        }
    }

    public enum ButtonStyle
    {
        /// <summary>
        /// Uses the the theme's accent colour.
        /// </summary>
        Primary,

        /// <summary>
        /// Uses neutral colours.
        /// </summary>
        Secondary,

        /// <summary>
        /// Text only.
        /// </summary>
        Text,
    }
}
