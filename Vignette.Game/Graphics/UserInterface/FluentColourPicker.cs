// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentColourPicker : HSVColourPicker
    {
        public FluentColourPicker()
        {
            Content.Spacing = new Vector2(0, 20);
            Content.Padding = new MarginPadding(20);
            Background.Colour = Colour4.Transparent;
        }

        protected override HueSelector CreateHueSelector() => new FluentHueSlider();

        protected override SaturationValueSelector CreateSaturationValueSelector() => new FluentSaturationSlider();

        protected class FluentSaturationSlider : SaturationValueSelector
        {
            public FluentSaturationSlider()
            {
                SelectionArea.Masking = true;
                SelectionArea.CornerRadius = 5.0f;
            }

            protected override Marker CreateMarker() => new FluentSaturationSliderMarker();

            private class FluentSaturationSliderMarker : Marker
            {
                private readonly Circle colourPreview;

                public FluentSaturationSliderMarker()
                {
                    InternalChild = new CircularContainer
                    {
                        Size = new Vector2(15),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Masking = true,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            colourPreview = new Circle
                            {
                                Size = new Vector2(0.8f),
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                            },
                        }
                    };
                }

                protected override void LoadComplete()
                {
                    base.LoadComplete();
                    Current.BindValueChanged(e => colourPreview.Colour = e.NewValue, true);
                }
            }
        }

        protected class FluentHueSlider : HueSelector
        {
            private Drawable nub;

            public FluentHueSlider()
            {
                ClearInternal(false);

                AddRangeInternal(new Drawable[]
                {
                    new CircularContainer
                    {
                        Depth = 1,
                        Masking = true,
                        AutoSizeAxes = Axes.Y,
                        RelativeSizeAxes = Axes.X,
                        Child = SliderBar.With(d => d.Height = 15),
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding { Right = 15 },
                        Child = nub,
                    },
                });
            }

            protected override Drawable CreateSliderNub() => nub = new FluentHueSliderNub();

            private class FluentHueSliderNub : CompositeDrawable
            {
                public FluentHueSliderNub()
                {
                    InternalChild = new CircularContainer
                    {
                        Size = new Vector2(15),
                        Masking = true,
                        Children = new Drawable[]
                        {
                            new ThemableBox
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = ThemeSlot.Gray10,
                            },
                            new ThemableCircle
                            {
                                RelativeSizeAxes = Axes.Both,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Colour = ThemeSlot.Gray190,
                                Size = new Vector2(0.8f),
                            },
                        },
                    };
                }
            }
        }
    }
}
