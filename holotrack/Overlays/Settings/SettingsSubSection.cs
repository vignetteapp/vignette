using holotrack.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace holotrack.Overlays.Settings
{
    public abstract class SettingsSubSection : FillFlowContainer
    {
        private TextFlowContainer header;
        private string headerText;
        public string HeaderText
        {
            get => headerText;
            set
            {
                headerText = value;
                header.AddText($"{headerText}\n", s => s.Font = HoloTrackFont.Black.With(size: 20));
            }
        }

        private string subHeaderText;
        public string SubHeaderText
        {
            get => subHeaderText;
            set
            {
                subHeaderText = value;
                header.AddText(subHeaderText, s => s.Font = HoloTrackFont.Medium.With(size: 16));
            }
        }


        protected FillFlowContainer Items;
        protected override Container<Drawable> Content => Items;

        public SettingsSubSection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
            Spacing = new Vector2(0, 5);

            InternalChildren = new Drawable[]
            {
                header = new TextFlowContainer(s => s.Font = HoloTrackFont.Default)
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                },
                Items = new FillFlowContainer
                {
                    Margin = new MarginPadding { Top = 5 },
                    Width = 200,
                    AutoSizeAxes = Axes.Y,
                    Spacing = new Vector2(0, 5),
                    Direction = FillDirection.Vertical,
                }
            };
        }
    }
}