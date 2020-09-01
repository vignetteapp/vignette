using holotrack.Graphics;
using holotrack.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace holotrack.Screens.Main
{
    public class SideMenuContent : Container
    {
        private readonly SpriteText headerSprite;
        private readonly HoloTrackScrollContainer content;
        protected override Container<Drawable> Content => content;

        private string header;
        public string Header
        {
            get => header;
            set
            {
                header = value;

                if (header != null)
                    headerSprite.Text = value;
            }
        }

        public SideMenuContent()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChild = new HoloTrackPanel
            {
                ContentPadding = new MarginPadding(10),
                RelativeSizeAxes = Axes.Both,
                Child = new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize),
                        new Dimension(GridSizeMode.Distributed),
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            headerSprite = new SpriteText
                            {
                                Text = header,
                                Font = HoloTrackFont.Control.With(size: 24, weight: "Bold"),
                                Margin = new MarginPadding { Bottom = 10 },
                            }
                        },
                        new Drawable[]
                        {
                            content = new HoloTrackScrollContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                            }
                        }
                    }
                }
            };
        }
    }
}