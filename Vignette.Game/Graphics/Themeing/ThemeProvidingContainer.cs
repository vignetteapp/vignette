// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using System;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Graphics.Themeing
{
    /// <summary>
    /// A container that provides a theme to its children <see cref="IThemable{T}"/>.
    /// </summary>
    [Cached(typeof(IThemeSource))]
    public class ThemeProvidingContainer : Container, IThemeSource
    {
        /// <summary>
        /// The current theme
        /// </summary>
        public readonly NonNullableBindable<Theme> Current = new NonNullableBindable<Theme>(Theme.Light);

        Bindable<Theme> IThemeSource.Current => Current;

        /// <summary>
        /// Invoked when the theme has been changed.
        /// </summary>
        public Action ThemeChanged { get; set; }

        protected override Container<Drawable> Content => content;

        private readonly Container content;

        public ThemeProvidingContainer()
        {
            InternalChild = new FluentContextMenuContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new FluentTooltipContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new FluentDropdownMenuContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = new PopoverContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = content = new Container
                            {
                                RelativeSizeAxes = Axes.Both
                            },
                        },
                    },
                },
            };

            Current.BindValueChanged(_ => ThemeChanged?.Invoke());
        }
    }
}
