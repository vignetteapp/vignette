// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Configuration;

namespace Vignette.Game.Graphics.Backgrounds
{
    public class BackgroundAdjustmentControl : CompositeDrawable
    {
        private Bindable<Vector2> offset;

        private Bindable<float> scale;

        private Bindable<float> rotation;

        private readonly Crosshair crosshair;

        public BackgroundAdjustmentControl()
        {
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black,
                    Alpha = 0.8f,
                },
                crosshair = new Crosshair
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0.9f,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            rotation = config.GetBindable<float>(VignetteSetting.BackgroundRotation);
            offset = config.GetBindable<Vector2>(VignetteSetting.BackgroundOffset);
            scale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
        }

        protected override bool OnDragStart(DragStartEvent e) => true;

        protected override void OnDrag(DragEvent e)
        {
            if (!e.ShiftPressed)
            {
                offset.Value += e.Delta;
            }
            else
            {
                rotation.Value += e.Delta.X;
            }
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            scale.Value += e.ScrollDelta.Y * ((BindableFloat)scale).Precision;
            return true;
        }

        private class Crosshair : UserBackground
        {
            private const float line_thickness = 2.5f;

            private const float line_width_start = 30;

            private const float line_width_shrink_rate = 5;

            private const float line_fade_rate = 0.15f;

            private const float line_gap = 50;

            private const int count = 7;

            public Crosshair()
            {
                for (int i = 0; i < count; i++)
                {
                    AddInternal(new Box
                    {
                        X = line_gap * i,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Height = line_width_start - (line_width_shrink_rate * i),
                        Width = line_thickness,
                        Alpha = 1.0f - (line_fade_rate * i),
                    });

                    AddInternal(new Box
                    {
                        Y = line_gap * i,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Height = line_thickness,
                        Width = line_width_start - (line_width_shrink_rate * i),
                        Alpha = 1.0f - (line_fade_rate * i),
                    });

                    if (i < 1)
                        continue;

                    AddInternal(new Box
                    {
                        X = -line_gap * i,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Height = line_width_start - (line_width_shrink_rate * i),
                        Width = line_thickness,
                        Alpha = 1.0f - (line_fade_rate * i),
                    });

                    AddInternal(new Box
                    {
                        Y = -line_gap * i,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Height = line_thickness,
                        Width = line_width_start - (line_width_shrink_rate * i),
                        Alpha = 1.0f - (line_fade_rate * i),
                    });
                }
            }
        }
    }
}
