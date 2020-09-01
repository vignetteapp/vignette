using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

namespace holotrack.Screens.Main
{
    public class Main : Screen
    {
        [BackgroundDependencyLoader]
        private void load(CubismAssetStore assets)
        {
            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Green,
                },
                new AdjustableCubismSprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Adjustable = true,
                    Asset = assets.Get(@"haru_greeter.haru_greeter.model3.json"),
                },
                new SideMenu()
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                },
            });
        }
    }
}