using vignette.Graphics;
using vignette.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace vignette.Overlays.Settings
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
                new VignetteSpriteText
                {
                    Text = Header,
                    Font = VignetteFont.Black.With(size: 20),
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