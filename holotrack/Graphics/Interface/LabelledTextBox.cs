using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace holotrack.Graphics.Interface
{
    public abstract class LabelledTextBox : GridContainer
    {
        public HoloTrackTextBox TextBox;
        protected abstract Drawable CreateLabel();

        public LabelledTextBox()
        {
            Height = 25;
            Masking = true;
            CornerRadius = 5;

            ColumnDimensions = new[]
            {
                new Dimension(GridSizeMode.AutoSize),
                new Dimension(GridSizeMode.Distributed),
            };

            Content = new[]
            {
                new Drawable[]
                {
                    new Container
                    {
                        AutoSizeAxes = Axes.X,
                        RelativeSizeAxes = Axes.Y,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = HoloTrackColor.Base,
                            },
                            CreateLabel(),
                        }
                    },
                    TextBox = new HoloTrackTextBox { RelativeSizeAxes = Axes.X }
                }
            };
        }
    }
}