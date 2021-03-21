// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.Input;

namespace Vignette.Application.Graphics.Containers
{
    public class ThemedScrollContainer : ThemedScrollContainer<Drawable>
    {
        public ThemedScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }
    }

    public class ThemedScrollContainer<T> : ScrollContainer<T>
        where T : Drawable
    {
        private readonly TriggerableComponent scrollTracker;

        public ThemedScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
            ScrollbarVisible = false;

            AddInternal(scrollTracker = new TriggerableComponent(1000));
            scrollTracker.Current.BindValueChanged((state) =>
            {
                if (state.NewValue)
                    Scrollbar.FadeOut(200, Easing.OutQuint);
                else
                    Scrollbar.FadeIn(200, Easing.OutQuint);
            });
        }

        protected override ScrollbarContainer CreateScrollbar(Direction direction) => new ThemedScrollbar(direction);

        protected override void OnUserScroll(float value, bool animated = true, double? distanceDecay = null)
        {
            base.OnUserScroll(value, animated, distanceDecay);
            scrollTracker.Trigger();
        }

        private class ThemedScrollbar : ScrollbarContainer
        {
            public ThemedScrollbar(Direction direction)
                : base(direction)
            {
                Child = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 5.0f,
                    Masking = true,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Width = 0.5f,
                    Alpha = 0.25f,
                    Child = new ThemedSolidBox
                    {
                        RelativeSizeAxes = Axes.Both,
                        ThemeColour = ThemeColour.NeutralPrimary,
                    }
                };
            }

            public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
                => this.ResizeTo(new Vector2(8) { [(int)ScrollDirection] = val }, duration, easing);
        }
    }
}
