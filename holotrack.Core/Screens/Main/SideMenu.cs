using holotrack.Core.Screens.Main.Menus;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace holotrack.Core.Screens.Main
{
    public class SideMenu : Container
    {
        private readonly CameraControl camera;
        private readonly SideMenuHeader header;
        private readonly SideMenuContent panel;

        public SideMenu()
        {
            Width = 200;
            Padding = new MarginPadding(10);
            RelativeSizeAxes = Axes.Y;

            Child = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.Distributed),
                    new Dimension(GridSizeMode.AutoSize)
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        header = new SideMenuHeader(),
                    },
                    new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding { Vertical = 10 },
                            Child = panel = new SideMenuContent(),
                        }
                    },
                    new Drawable[]
                    {
                        camera = new CameraControl(),
                    },
                }
            };

            header.Current.ValueChanged += onTabChanged;
        }

        private void onTabChanged(ValueChangedEvent<SideMenuTab> tab)
        {
            var current = tab.NewValue;
            panel.Child = current.CreateContent();
            panel.Header = current.Name;
        }
    }
}