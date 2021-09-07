// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Settings.Sections
{
    public class FooterSection : SettingsSection
    {
        public FooterSection()
        {
            Child = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                Height = 100,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 10),
                Children = new Drawable[]
                {
                    new ThemableSpriteIcon
                    {
                        Size = new Vector2(24),
                        Icon = VignetteFont.Logo,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = ThemeSlot.Gray60,
                    },
                    new ThemableTextFlowContainer(s => s.Colour = ThemeSlot.Gray60)
                    {
                        Text = "Copyright 2020 - 2021\nVignette Project",
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        TextAnchor = Anchor.Centre,
                    },
                },
            };
        }
    }
}
