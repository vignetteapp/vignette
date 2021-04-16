// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class TextBox : osu.Framework.Graphics.UserInterface.TextBox
    {
        private Bindable<Theme> theme;

        private SpriteText placeholderText;

        private readonly Container border;

        private Colour4 colourFocussed;

        private Colour4 colourUnfocussed;

        public TextBox()
        {
            Height = 40;

            TextContainer.Height = 0.5f;
            TextContainer.Padding = new MarginPadding { Horizontal = 5 };
            Add(border = new Container
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 5,
                BorderThickness = 2,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent,
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                colourFocussed = e.NewValue.NeutralTertiary;
                placeholderText.Colour = border.BorderColour = colourUnfocussed = e.NewValue.NeutralPrimary;
            }, true);
        }

        protected override Caret CreateCaret() => new TextBoxCaret();

        protected override SpriteText GetDrawableCharacter(char c) => new TextBoxCharacter
        {
            Font = SegoeUI.Regular.With(size: CalculatedTextSize),
            Text = c.ToString(),
        };

        protected override SpriteText CreatePlaceholder() => placeholderText = new SpriteText
        {
            Font = SegoeUI.Regular,
        };

        protected override void NotifyInputError()
        {
        }

        protected override void OnFocus(FocusEvent e)
        {
            base.OnFocus(e);
            border.BorderColour = colourFocussed;
        }

        protected override void OnFocusLost(FocusLostEvent e)
        {
            base.OnFocusLost(e);
            border.BorderColour = colourUnfocussed;
        }

        protected class TextBoxCharacter : SpriteText
        {
            private Bindable<Theme> theme;

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> theme)
            {
                this.theme = theme.GetBoundCopy();
                this.theme.BindValueChanged(e =>
                {
                    Colour = e.NewValue.White;
                }, true);
            }
        }

        protected class TextBoxCaret : Caret
        {
            private const float caret_move_time = 60;

            private Bindable<Theme> theme;

            private Colour4 selectionColour;

            private Colour4 caretColour;

            private readonly Box cursor;

            public float CaretWidth { get; set; }

            public TextBoxCaret()
            {
                RelativeSizeAxes = Axes.Y;

                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                InternalChild = cursor = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                };
            }

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> theme)
            {
                this.theme = theme.GetBoundCopy();
                this.theme.BindValueChanged(e =>
                {
                    selectionColour = e.NewValue.AccentPrimary;
                    caretColour = e.NewValue.Black;
                }, true);
            }

            public override void DisplayAt(Vector2 position, float? selectionWidth)
            {
                if (selectionWidth != null)
                {
                    this.MoveTo(new Vector2(position.X, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(selectionWidth.Value + CaretWidth / 2, caret_move_time, Easing.Out);
                    cursor.Alpha = 1.0f;
                    cursor.Colour = selectionColour;
                    cursor.ClearTransforms();
                }
                else
                {
                    this.MoveTo(new Vector2(position.X - CaretWidth / 2, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(CaretWidth, caret_move_time, Easing.Out);
                    cursor.Colour = caretColour;
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
