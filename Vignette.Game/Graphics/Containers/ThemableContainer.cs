// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osuTK;
using System.Collections.Generic;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Containers
{
    public class ThemableContainer : ThemableContainer<Drawable>
    {
    }

    public class ThemableContainer<T> : ThemableDrawable<Container<T>>
        where T : Drawable
    {
        private ThemeSlot borderColour;

        public new ThemeSlot BorderColour
        {
            get => borderColour;
            set
            {
                if (borderColour == value)
                    return;

                borderColour = value;
                ScheduleThemeChange();
            }
        }

        public new float BorderThickness
        {
            get => Target.BorderThickness;
            set => Target.BorderThickness = value;
        }

        public new float CornerRadius
        {
            get => Target.CornerRadius;
            set => Target.CornerRadius = value;
        }

        private bool shadow;

        public bool Shadow
        {
            get => shadow;
            set
            {
                if (shadow == value)
                    return;

                shadow = value;
                ScheduleThemeChange();
            }
        }

        private float shadowRadius = 5;

        public float ShadowRadius
        {
            get => shadowRadius;
            set
            {
                if (shadowRadius == value)
                    return;

                shadowRadius = value;
                ScheduleThemeChange();
            }
        }

        private float shadowAlpha = 0.1f;

        public float ShadowAlpha
        {
            get => shadowAlpha;
            set
            {
                if (shadowAlpha == value)
                    return;

                shadowAlpha = value;
                ScheduleThemeChange();
            }
        }

        public T Child
        {
            get => Target.Child;
            set => Target.Child = value;
        }

        public IReadOnlyList<T> Children
        {
            get => Target.Children;
            set => Target.Children = value;
        }

        public ThemableContainer(bool attached = true)
            : base(attached)
        {
        }

        public void Add(T drawable) => Target.Add(drawable);

        public void AddRange(IEnumerable<T> range) => Target.AddRange(range);

        public void Clear() => Target.Clear();

        public bool Remove(T drawable) => Target.Remove(drawable);

        protected override Container<T> CreateDrawable() => new Container<T>
        {
            RelativeSizeAxes = Axes.Both,
            Masking = true,
        };

        protected override void ThemeChanged(Theme theme)
        {
            Target.BorderColour = theme.Get(BorderColour);

            if (!Shadow)
                return;

            Target.EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Offset = new Vector2(0, 2),
                Colour = Colour4.Black.Opacity(shadowAlpha),
                Hollow = true,
                Radius = shadowRadius,
                Roundness = 5,
            };
        }
    }
}
