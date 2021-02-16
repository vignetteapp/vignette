// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class VignetteTextBox : TextBox
    {
        private readonly OutlinedBox border;

        public VignetteTextBox()
        {
            Height = 35;
            TextFlow.Padding = new MarginPadding { Vertical = 5 };
            TextContainer.Height = 0.75f;

            Add(border = new OutlinedBox
            {
                Depth = 1,
                ThemeColour = ThemeColour.NeutralTertiary,
                CornerRadius = VignetteStyle.CornerRadius,
                RelativeSizeAxes = Axes.Both,
            });
        }

        protected override Caret CreateCaret() => new VignetteCaret { CaretWidth = 1 };

        protected override Drawable GetDrawableCharacter(char c) => new VignetteSpriteText
        {
            Text = c.ToString(),
            Font = VignetteFont.Regular.With(size: 16),
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
        };

        protected override SpriteText CreatePlaceholder() => new VignetteSpriteText
        {
            Font = VignetteFont.Regular,
            ThemeColour = ThemeColour.NeutralQuaternary,
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
        };

        protected override void NotifyInputError()
        {
        }

        private class VignetteCaret : Caret
        {
            private const float caret_move_time = 60;

            private readonly VignetteBox cursor;

            public float CaretWidth { get; set; }

            public VignetteCaret()
            {
                RelativeSizeAxes = Axes.Y;
                Size = new Vector2(1, 0.9f);

                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                InternalChild = cursor = new VignetteBox
                {
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.Black,
                };
            }

            public override void DisplayAt(Vector2 position, float? selectionWidth)
            {
                if (selectionWidth != null)
                {
                    this.MoveTo(new Vector2(position.X, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(selectionWidth.Value + CaretWidth / 2, caret_move_time, Easing.Out);
                    cursor.Alpha = 1.0f;
                    cursor.ThemeColour = ThemeColour.ThemePrimary;
                    cursor.ClearTransforms();
                }
                else
                {
                    this.MoveTo(new Vector2(position.X - CaretWidth / 2, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(CaretWidth, caret_move_time, Easing.Out);
                    cursor.ThemeColour = ThemeColour.Black;
                    cursor
                        .FadeOutFromOne(100, Easing.OutQuint)
                        .Delay(500)
                        .FadeInFromZero(100, Easing.OutQuint)
                        .Loop(500);
                }
            }
        }
    }
}
