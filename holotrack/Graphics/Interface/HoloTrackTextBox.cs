using holotrack.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackTextBox : BasicTextBox
    {
        public HoloTrackTextBox()
        {
            Height = 25;
            Masking = true;
            CornerRadius = 5;

            BackgroundUnfocused = HoloTrackColor.Darkest;
            BackgroundFocused = HoloTrackColor.Darkest;
            BackgroundCommit = Colour4.CadetBlue;
        }

        protected override SpriteText CreatePlaceholder() => new HoloTrackSpriteText { Colour = Colour4.Gray };
        protected override Drawable GetDrawableCharacter(char c) => new HoloTrackSpriteText { Text = c.ToString() };
        protected override Caret CreateCaret() => new HoloTrackCaret { CaretWidth = CaretWidth };

        private class HoloTrackCaret : Caret
        {
            private const float caret_move_time = 60;
            public float CaretWidth { get; set; }

            public HoloTrackCaret()
            {
                RelativeSizeAxes = Axes.Y;
                Size = new Vector2(1, 0.9f);

                Colour = Colour4.Transparent;
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                Masking = true;
                CornerRadius = 1;

                InternalChild = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                };
            }

            public override void DisplayAt(Vector2 position, float? selectionWidth)
            {
                if (selectionWidth != null)
                {
                    this.MoveTo(new Vector2(position.X, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(selectionWidth.Value + CaretWidth / 2, caret_move_time, Easing.Out);
                    this
                        .FadeTo(0.5f, 200, Easing.Out)
                        .FadeColour(Colour4.CadetBlue, 200, Easing.Out);
                }
                else
                {
                    this.MoveTo(new Vector2(position.X - CaretWidth / 2, position.Y), 60, Easing.Out);
                    this.ResizeWidthTo(CaretWidth, caret_move_time, Easing.Out);
                    this.FadeColour(Colour4.White, 200, Easing.Out);
                }
            }
        }
    }
}