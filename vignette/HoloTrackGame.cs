using vignette.Input;
using vignette.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;

namespace vignette
{
    public class VignetteGame : VignetteGameBase
    {
        private BufferedContainer globalBufferedContainer;
        public BufferedContainerView<Drawable> CreateView() => globalBufferedContainer?.CreateView().With(d =>
        {
            d.RelativeSizeAxes = Axes.Both;
            d.SynchronisedDrawQuad = true;
            d.DisplayOriginalEffects = true;
        });

        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs(this);

            Add(new DrawSizePreservingFillContainer
            {
                Child = new VignetteKeyBindingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = globalBufferedContainer = new BufferedContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = new ScreenStack(new Main()),
                    },
                },
            });
        }
    }
}