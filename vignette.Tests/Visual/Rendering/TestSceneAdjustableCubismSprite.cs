using vignette.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Testing;

namespace vignette.Tests.Visual.Rendering
{
    public class TestSceneAdjustableCubismSprite : TestScene
    {
        [BackgroundDependencyLoader]
        private void load(CubismAssetStore assets)
        {
            Add(new AdjustableCubismSprite
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}