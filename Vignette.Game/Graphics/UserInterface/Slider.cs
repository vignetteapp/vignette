// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Themes;

namespace Vignette.Game.Graphics.UserInterface
{
    public class Slider<T> : SliderBar<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        private Bindable<Theme> theme;

        private readonly Box background;

        private readonly Box accent;

        private readonly Circle nub;

        private readonly Container nubContainer;

        public Slider()
        {
            Height = 40;
            RangePadding = 3;
            Children = new Drawable[]
            {
                background = new Box
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.X,
                    Height = 2,
                },
                accent = new Box
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.X,
                    Height = 2,
                },
                nubContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = nub = new Circle
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.Centre,
                        Size = new Vector2(10),
                        RelativePositionAxes = Axes.X,
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                background.Colour = e.NewValue.NeutralDark;
                accent.Colour = nub.Colour = e.NewValue.AccentPrimary;
            }, true);
        }

        protected override void UpdateValue(float value)
        {
            accent.Width = nub.X = value;
        }

        protected override void Update()
        {
            base.Update();
            nubContainer.Padding = new MarginPadding { Horizontal = RangePadding };
        }
    }
}
