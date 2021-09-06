// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A button where an action is performed when clicked on.
    /// </summary>
    public class FluentButton : FluentButtonBase, IHasText, IHasIcon, IHasTooltip
    {
        /// <summary>
        /// Gets or sets the tooltip text shown when hovered over.
        /// </summary>
        public virtual LocalisableString TooltipText { get; set; }

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
                if (Label == null)
                    createLabelFlow();

                if (text == null)
                {
                    Label.Insert(1, text = new ThemableSpriteText
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
                if (Label == null)
                    createLabelFlow();

                if (icon == null)
                {
                    Label.Insert(0, icon = new ThemableSpriteIcon
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

        protected FillFlowContainer Label;

        private readonly ThemableBox background;
        private ThemableSpriteText text;
        private ThemableSpriteIcon icon;

        public FluentButton()
        {
            Height = 32;
            Masking = true;
            CornerRadius = 5.0f;
            InternalChild = background = new ThemableBox
            {
                RelativeSizeAxes = Axes.Both,
            };
        }

        protected override void LoadComplete()
        {
            updateStyle();
            base.LoadComplete();
        }

        protected override void UpdateBackground(ThemeSlot slot)
            => background.Colour = slot;

        protected override void UpdateLabel(ThemeSlot slot)
        {
            if (text != null)
                text.Colour = slot;

            if (icon != null)
                icon.Colour = slot;
        }

        private void createLabelFlow()
        {
            Add(Label = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(8, 0),
                Margin = new MarginPadding { Horizontal = 8 },
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        private void updateStyle()
        {
            switch (Style)
            {
                case ButtonStyle.Primary:
                    LabelResting = ThemeSlot.White;
                    BackgroundResting = ThemeSlot.AccentPrimary;
                    BackgroundHovered = ThemeSlot.AccentDarkAlt;
                    BackgroundPressed = ThemeSlot.AccentDark;
                    break;

                case ButtonStyle.Secondary:
                    LabelResting = ThemeSlot.Gray190;
                    BackgroundResting = ThemeSlot.White;
                    BackgroundHovered = ThemeSlot.Gray30;
                    BackgroundPressed = ThemeSlot.Gray40;
                    break;

                case ButtonStyle.Text:
                    LabelResting = ThemeSlot.Gray190;
                    BackgroundResting = ThemeSlot.Transparent;
                    BackgroundHovered = ThemeSlot.Gray30;
                    BackgroundPressed = ThemeSlot.Gray40;
                    break;
            }

            LabelDisabled = ThemeSlot.Gray90;
            BackgroundDisabled = Style != ButtonStyle.Text ? ThemeSlot.Gray20 : ThemeSlot.Transparent;

            UpdateState();
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
