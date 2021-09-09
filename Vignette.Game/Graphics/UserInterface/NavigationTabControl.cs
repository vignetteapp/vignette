// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Linq;
using osu.Framework.Bindables;
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
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationTabControl<T> : TabControl<T>
    {
        public NavigationTabControl()
        {
            Current.BindDisabledChanged(handleEnabledState, true);
        }

        protected override Dropdown<T> CreateDropdown() => null;

        protected override void AddTabItem(TabItem<T> tab, bool addToDropdown = true)
        {
            // Band-aid for now until https://github.com/ppy/osu-framework/issues/4754 is resolved
            tab.Enabled.Value = true;
            base.AddTabItem(tab, addToDropdown);
        }

        private void handleEnabledState(bool disabled)
        {
            foreach (var tab in AllTabs.Cast<NavigationTabItem>())
                tab.Enabled.Value = !disabled;
        }

        protected abstract class NavigationTabItem : TabItem<T>
        {
            protected virtual LocalisableString? Text => null;

            protected virtual IconUsage? Icon => null;

            protected virtual bool ForceTextMargin => true;

            protected virtual float TextLeftMargin => 35;

            protected readonly ThemableCircle Highlight;
            protected readonly Container LabelContainer;
            protected readonly ThemableEffectBox Background;
            private readonly ThemableSpriteText text;
            private readonly ThemableSpriteIcon icon;

            public NavigationTabItem(T value)
                : base(value)
            {
                Highlight = new ThemableCircle();

                Add(Background = new ThemableEffectBox
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 5.0f,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = ThemeSlot.Transparent,
                });

                Add(LabelContainer = new Container
                {
                    Margin = new MarginPadding { Horizontal = 12 },
                    AutoSizeAxes = Axes.X,
                    RelativeSizeAxes = Axes.Y,
                });

                if (Icon != null)
                {
                    LabelContainer.Add(icon = new ThemableSpriteIcon
                    {
                        Icon = Icon ?? default,
                        Size = new Vector2(16),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                if (Text != null)
                {
                    LabelContainer.Add(text = new ThemableSpriteText
                    {
                        Text = Text ?? string.Empty,
                        Font = SegoeUI.Regular.With(size: 18),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                if ((Icon != null && Text != null) || ForceTextMargin)
                    text.Margin = new MarginPadding { Left = TextLeftMargin };

                Active.ValueChanged += _ => updateColours();
                Enabled.ValueChanged += _ => updateColours();

                updateColours();
            }

            private void updateColours()
            {
                if (Enabled.Value)
                {
                    if (isPressed || Active.Value)
                        Background.Colour = ThemeSlot.Gray30;
                    else if (IsHovered)
                        Background.Colour = ThemeSlot.Gray20;
                    else
                        Background.Colour = ThemeSlot.Transparent;

                    Highlight.Colour = ThemeSlot.AccentPrimary;

                    if (icon != null)
                        icon.Colour = ThemeSlot.Black;

                    if (text != null)
                        text.Colour = ThemeSlot.Black;
                }
                else
                {
                    if (Active.Value)
                        Background.Colour = ThemeSlot.Gray10;

                    Highlight.Colour = ThemeSlot.AccentDarker;

                    if (icon != null)
                        icon.Colour = ThemeSlot.Gray60;

                    if (text != null)
                        text.Colour = ThemeSlot.Gray60;
                }
            }

            private bool isPressed;

            protected override bool OnMouseDown(MouseDownEvent e)
            {
                isPressed = true;
                updateColours();
                return base.OnMouseDown(e);
            }

            protected override void OnMouseUp(MouseUpEvent e)
            {
                isPressed = false;
                updateColours();
                base.OnMouseUp(e);
            }

            protected override bool OnHover(HoverEvent e)
            {
                updateColours();
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                updateColours();
                base.OnHoverLost(e);
            }
        }
    }
}
