// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Graphics.Interface
{
    public class VignetteCheckbox : Checkbox, IHasText
    {
        private readonly VignetteSpriteText spriteText;

        private readonly VignetteSpriteIcon spriteIcon;

        private readonly OutlinedBox outlinedBox;

        private readonly VignetteBox filledBox;

        private readonly Container checkbox;

        private const float checkbox_height = 25.0f;

        public string Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value;
        }

        [Resolved(CanBeNull = true)]
        private VignetteColour style { get; set; }

        public VignetteCheckbox()
        {
            Height = checkbox_height;
            AutoSizeAxes = Axes.X;
            AddRange(new Drawable[]
            {
                checkbox = new Container
                {
                    Size = new Vector2(checkbox_height),
                    Masking = true,
                    EdgeEffect = new EdgeEffectParameters
                    {
                        Type = EdgeEffectType.Glow,
                        Radius = 2.5f,
                        Colour = Colour4.Transparent,
                    },
                    CornerRadius = VignetteStyle.CornerRadius,
                    Children = new Drawable[]
                    {
                        outlinedBox = new OutlinedBox
                        {
                            BorderThickness = VignetteStyle.BorderThickness,
                            // the parent's corner radius is ignored apparently
                            CornerRadius = VignetteStyle.CornerRadius,
                            Level = 4,
                            Size = new Vector2(checkbox_height),
                        },
                        filledBox = new VignetteBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colouring = Colouring.Accent,
                            Alpha = 0.0f,
                        },
                        spriteIcon = new VignetteSpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colouring = Colouring.Background,
                            Alpha = 0.0f,
                            Icon = FontAwesome.Solid.Check,
                            Size = new Vector2(checkbox_height - 15),
                        },
                    }
                },
                spriteText = new VignetteSpriteText
                {
                    Margin = new MarginPadding { Left = checkbox_height + 10 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Level = 4,
                    Font = VignetteFont.SemiBold.With(size: 16),
                }
            });
        }

        protected override void OnUserChange(bool value)
        {
            spriteIcon.Alpha = filledBox.Alpha = value ? 1.0f : 0.0f;
            spriteIcon.Level = value ? 0 : 6;
            outlinedBox.Alpha = value ? 0.0f : 1.0f;

            if (value)
            {
                checkbox
                    .FadeEdgeEffectTo(style?.Accent.Value ?? Colour4.White)
                    .Delay(50)
                    .FadeEdgeEffectTo(0.0f, 2000, Easing.OutQuint);
            }
            else
                checkbox.FadeEdgeEffectTo(0.0f);
        }
    }
}
