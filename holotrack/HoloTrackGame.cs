using holotrack.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osuTK;

namespace holotrack
{
    public class HoloTrackGame : HoloTrackGameBase
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1280, 720),
                Strategy = DrawSizePreservationStrategy.Maximum,
                Child = new ScreenStack(new Main()),
            });
        }
    }
}