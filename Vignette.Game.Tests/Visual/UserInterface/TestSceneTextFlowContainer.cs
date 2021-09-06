// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneTextFlowContainer : UserInterfaceTestScene
    {
        private readonly ThemableTextFlowContainer flow;

        public TestSceneTextFlowContainer()
        {
            Add(flow = new ThemableTextFlowContainer
            {
                Margin = new MarginPadding(10),
                AutoSizeAxes = Axes.Both,
            });

            flow.AddText("Welcome to", t => t.Font = SegoeUI.Black.With(size: 24));
            flow.AddText(" Vignette", t => t.Font = SegoeUI.Black.With(size: 24));

            flow.NewParagraph();
            flow.NewParagraph();

            flow.AddText("The open-source toolkit for VTubers. Made with");
            flow.AddText(" <3 ", t => t.Font = SegoeUI.Black);
            flow.AddText("with osu!framework.");
        }
    }
}
