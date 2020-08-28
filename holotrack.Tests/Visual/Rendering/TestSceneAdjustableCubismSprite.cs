using holotrack.Core.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Testing;

namespace holotrack.Tests.Visual.Rendering
{
    public class TestSceneAdjustableCubismSprite : TestScene
    {
        [BackgroundDependencyLoader]
        private void load(CubismAssetStore assets)
        {
            Add(new AdjustableCubismSprite
            {
                RelativeSizeAxes = Axes.Both,
                Asset = assets.Get(@"haru.haru.model3.json"),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Adjustable = true,
            });
            
        }
    }
}