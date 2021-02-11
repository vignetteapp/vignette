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
        protected override Container<Drawable> Content => content;

        private readonly Container content;

        public VignetteApplicationBase()
        {
            AddRangeInternal(new[]
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
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(VignetteApplicationBase).Assembly), @"Resources"));
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), @"Resources"));

            AddFont(Resources, @"Fonts/Raleway");
            AddFont(Resources, @"Fonts/Raleway-Black");
            AddFont(Resources, @"Fonts/Raleway-BlackItalic");
            AddFont(Resources, @"Fonts/Raleway-Bold");
            AddFont(Resources, @"Fonts/Raleway-BoldItalic");
            AddFont(Resources, @"Fonts/Raleway-ExtraBold");
            AddFont(Resources, @"Fonts/Raleway-ExtraBoldItalic");
            AddFont(Resources, @"Fonts/Raleway-Light");
            AddFont(Resources, @"Fonts/Raleway-LightItalic");
            AddFont(Resources, @"Fonts/Raleway-Medium");
            AddFont(Resources, @"Fonts/Raleway-MediumItalic");
            AddFont(Resources, @"Fonts/Raleway-SemiBold");
            AddFont(Resources, @"Fonts/Raleway-SemiBoldItalic");
            AddFont(Resources, @"Fonts/Raleway-Thin");
            AddFont(Resources, @"Fonts/Raleway-ThinItalic");
        }
    }
}
