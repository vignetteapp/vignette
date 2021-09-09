// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens.Stage.Edit
{
    public class EditingStrip : VisibilityContainer
    {
        protected override bool StartHidden => true;

        private readonly ThemableTextFlowContainer flow;

        public EditingStrip()
        {
            Height = 28;
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            RelativeSizeAxes = Axes.X;
            Children = new Drawable[]
            {
                new ThemableBox
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.WarningBackground,
                },
                flow = new ThemableTextFlowContainer(s => s.Colour = ThemeSlot.Warning)
                {
                    TextAnchor = Anchor.CentreLeft,
                    Padding = new MarginPadding { Left = 30 },
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.Both,
                },
                new ThemableSpriteIcon
                {
                    Icon = SegoeFluent.Info,
                    Size = new Vector2(16),
                    Colour = ThemeSlot.Warning,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Left = 10 },
                }
            };

            flow.AddText("Edit Mode", s => s.Font = SegoeUI.Bold);
            flow.AddText(" — ");
            flow.AddText("Drag", s => s.Font = SegoeUI.Bold);
            flow.AddText(" to move and ");

            flow.AddText("Scroll", s => s.Font = SegoeUI.Bold);
            flow.AddText(" to scale");
        }

        protected override void PopIn()
        {
            this.MoveToY(0, 300, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this.MoveToY(28, 300, Easing.OutQuint);
        }
    }
}
