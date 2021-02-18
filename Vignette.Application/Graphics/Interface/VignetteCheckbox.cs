// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class VignetteCheckbox : Checkbox, IHasText
    {
        private readonly VignetteSpriteText spriteText;

        private readonly VignetteSpriteIcon spriteIcon;

        private readonly OutlinedBox outlinedBox;

        private readonly VignetteBox filledBox;

        private const float checkbox_height = 25.0f;

        public string Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value;
        }

        public VignetteCheckbox()
        {
            Height = checkbox_height;
            AutoSizeAxes = Axes.X;
            AddRange(new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(checkbox_height),
                    Masking = true,
                    EdgeEffect = new EdgeEffectParameters
                    {
                        Type = EdgeEffectType.Glow,
                        Radius = 2.5f,
                        Colour = Colour4.Transparent,
                    },
                    CornerRadius = VignetteStyle.CornerRadius * 1.5f,
                    Children = new Drawable[]
                    {
                        outlinedBox = new OutlinedBox
                        {
                            // the parent's corner radius is ignored apparently
                            CornerRadius = VignetteStyle.CornerRadius * 1.5f,
                            ThemeColour = ThemeColour.NeutralPrimary,
                            Size = new Vector2(checkbox_height),
                        },
                        filledBox = new VignetteBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            ThemeColour = ThemeColour.ThemePrimary,
                            Alpha = 0.0f,
                        },
                        spriteIcon = new VignetteSpriteIcon
                        {
                            ThemeColour = ThemeColour.White,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0.0f,
                            Icon = FontAwesome.Solid.Check,
                            Size = new Vector2(checkbox_height - 15),
                        },
                    }
                },
                spriteText = new VignetteSpriteText
                {
                    ThemeColour = ThemeColour.NeutralPrimary,
                    Margin = new MarginPadding { Left = checkbox_height + 10 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Font = VignetteFont.Regular.With(size: 16),
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
