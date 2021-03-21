// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Screens.Main
{
    public abstract class ToolbarSection : PresentationSlide
    {
        public virtual string Title => GetType().Name;

        public virtual IconUsage Icon => FluentSystemIcons.Filled.Question24;

        protected readonly FillFlowContainer FlowContent;

        protected override Container<Drawable> Content => FlowContent;

        public ToolbarSection()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;

            InternalChild = new ThemedScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(20),
                Children = new Drawable[]
                {
                    new ThemedSpriteText
                    {
                        Text = Title.ToUpperInvariant(),
                        Font = SegoeUI.Bold.With(size: 24)
                    },
                    FlowContent = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        LayoutDuration = 200,
                        AutoSizeAxes = Axes.Y,
                        LayoutEasing = Easing.OutQuint,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(0, 10),
                        Margin = new MarginPadding { Top = 40 }
                    }
                }
            };
        }

        public override void OnEntering()
        {
            this
                .ScaleTo(1, 200, Easing.OutQuint)
                .FadeIn(200, Easing.OutQuint);
        }

        public override void OnExiting()
        {
            this
                .ScaleTo(0.98f, 200, Easing.OutQuint)
                .FadeOut(200, Easing.OutQuint);
        }
    }
}
