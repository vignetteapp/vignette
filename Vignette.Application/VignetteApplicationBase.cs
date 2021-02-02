// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using Vignette.Application.Live2D.Resources;

namespace Vignette.Application
{
    public class VignetteApplicationBase : Game
    {
        protected new Container<Drawable> Content => content;

        private readonly Container content;

        public VignetteApplicationBase()
        {
            AddRange(new[]
            {
                new SafeAreaContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new DrawSizePreservingFillContainer
                    {
                        Child = content = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), "Resources"));
        }
    }
}
