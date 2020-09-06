using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Graphics.Interface
{
    public abstract class LabelledTextBox : GridContainer, IHasCurrentValue<string>
    {
        protected abstract Drawable CreateLabel();
        protected virtual TextBox CreateTextBox() => new HoloTrackTextBox();

        public TextBox TextBox { get; protected set; }

        public Bindable<string> Current
        {
            get => TextBox.Current;
            set => TextBox.Current = value;
        }

        public string Text
        {
            get => TextBox.Text;
            set => TextBox.Text = value;
        }

        public string PlaceholderText
        {
            get => TextBox.PlaceholderText;
            set => TextBox.PlaceholderText = value;
        }

        public bool CommitOnFocusLost
        {
            get => TextBox.CommitOnFocusLost;
            set => TextBox.CommitOnFocusLost = value;
        }

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
                    TextBox = CreateTextBox().With(t => t.RelativeSizeAxes = Axes.X),
                }
            };
        }
    }
}