// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Shapes;

namespace Vignette.Application.Configuration.Settings
{
    public class SettingPanel : VisibilityContainer
    {
        private FillFlowContainer<SettingSection> sectionFlow;

        private VignetteScrollContainer scrollContainer;

        public SettingPanel(IEnumerable<SettingSection> sections)
        {
            Children = new Drawable[]
            {
                new VignetteBox { RelativeSizeAxes = Axes.Both },
                scrollContainer = new VignetteScrollContainer
                {
                    Width = 640,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Y,
                    Child = sectionFlow = new FillFlowContainer<SettingSection>
                    {
                        Direction = FillDirection.Vertical,
                        AutoSizeAxes = Axes.Y,
                        RelativeSizeAxes = Axes.X,
                    },
                },
            };

            foreach (var section in sections)
                sectionFlow.Add(section);
        }

        public void ScrollToSection(SettingSection section)
        {
            var toSection = sectionFlow.Children.FirstOrDefault(s => section.GetType() == s.GetType());
            scrollContainer.ScrollTo(toSection);
        }

        protected override void PopIn()
        {
            this
                .ScaleTo(0.8f, 0)
                .FadeIn(200, Easing.OutQuint)
                .ScaleTo(1, 200, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this
                .FadeOut(200, Easing.OutQuint)
                .ScaleTo(0.8f, 200, Easing.OutQuint);
        }
    }
}
