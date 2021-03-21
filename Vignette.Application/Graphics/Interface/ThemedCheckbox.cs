// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class ThemedCheckbox : Checkbox, IHasText
    {
        private readonly ThemedSpriteText spriteText;

        private readonly ThemedSpriteIcon spriteIcon;

        private readonly ThemedOutlinedBox outlinedBox;

        private readonly ThemedSolidBox filledBox;

        private const float checkbox_height = 18.0f;

        public LocalisableString Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value;
        }

        public ThemedCheckbox()
        {
            Height = checkbox_height;
            AutoSizeAxes = Axes.X;
            AddRange(new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(checkbox_height),
                    Masking = true,
                    CornerRadius = 2.5f,
                    Children = new Drawable[]
                    {
                        outlinedBox = new ThemedOutlinedBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            ThemeColour = ThemeColour.NeutralPrimary,
                            CornerRadius = 2.5f,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        },
                        filledBox = new ThemedSolidBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            ThemeColour = ThemeColour.ThemePrimary,
                            Alpha = 0.0f,
                        },
                        spriteIcon = new ThemedSpriteIcon
                        {
                            ThemeColour = ThemeColour.NeutralPrimary,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0.0f,
                            Icon = FluentSystemIcons.Filled.Checkmark16,
                            Size = new Vector2(10),
                        },
                    }
                },
                spriteText = new ThemedSpriteText
                {
                    ThemeColour = ThemeColour.NeutralPrimary,
                    Margin = new MarginPadding { Left = checkbox_height + 10 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Font = SegoeUI.Regular.With(size: 16),
                }
            });
        }

        protected override void OnUserChange(bool value)
        {
            spriteIcon.Alpha = filledBox.Alpha = value ? 1.0f : 0.0f;
            outlinedBox.Alpha = value ? 0.0f : 1.0f;
        }
    }
}
