// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Settings.Sections
{
    public abstract class WorkInProgressSection : SettingsSection
    {
        public WorkInProgressSection()
        {
            Add(new Container
            {
                Height = 300,
                RelativeSizeAxes = Axes.X,
                Children = new Drawable[]
                {
                    new ThemableBox
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = ThemeSlot.Gray20,
                    },
                    new FillFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Both,
                        Direction = FillDirection.Vertical,
                        Children = new Drawable[]
                        {
                            new ThemableSpriteIcon
                            {
                                Size = new Vector2(32),
                                Icon = FluentSystemIcons.Info24,
                                Colour = ThemeSlot.Black,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                            new ThemableSpriteText
                            {
                                Text = $"{Header} is currently not ready",
                                Font = SegoeUI.Bold.With(size: 22),
                                Colour = ThemeSlot.Black,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Margin = new MarginPadding { Top = 10 },
                            },
                            new ThemableSpriteText
                            {
                                Text = "please check back later!",
                                Font = SegoeUI.Regular.With(size: 16),
                                Colour = ThemeSlot.Black,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                        },
                    }
                },
            });
        }
    }
}
