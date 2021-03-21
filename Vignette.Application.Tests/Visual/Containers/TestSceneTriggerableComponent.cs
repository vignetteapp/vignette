// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Input;

namespace Vignette.Application.Tests.Visual.Containers
{
    public class TestSceneTriggerableComponent : TestScene
    {
        private readonly Box visualizer;

        private readonly TriggerableComponent tracker;

        public TestSceneTriggerableComponent()
        {
            AddRange(new Drawable[]
            {
                tracker =  new TriggerableComponent(1000),
                visualizer = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(256)
                }
            });

            tracker.Current.BindValueChanged((e) => visualizer.Colour = e.NewValue ? Colour4.Green : Colour4.Red, true);

            AddStep("trigger", () => tracker.Trigger());
        }
    }
}
