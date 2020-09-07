using holotrack.Graphics;
using holotrack.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace holotrack.Overlays.Settings
{
    public abstract class SettingsSubsection : FillFlowContainer
    {
        public abstract string Header { get; }

        protected FillFlowContainer Items;
        protected override Container<Drawable> Content => Items;

        public SettingsSubsection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
            Spacing = new Vector2(0, 5);

            InternalChildren = new Drawable[]
            {
                new HoloTrackSpriteText
                {
                    Text = Header,
                    Font = HoloTrackFont.Black.With(size: 20),
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