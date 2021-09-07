// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class AvatarSection : SettingsSection
    {
        public override LocalisableString Label => "Avatar";

        public override IconUsage Icon => SegoeFluent.Person;

        public AvatarSection() => Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Child = new SettingsFileBrowser
                    {
                        Label = "Location",
                    },
                },
                new SettingsSubSection
                {
                    Children = new Drawable[]
                    {
                        new PositionItem(),
                        new SettingsSlider<float>
                        {
                            Icon = SegoeFluent.SlideSize,
                            Label = "Scale",
                            Current = new BindableFloat
                            {
                                MinValue = 0,
                                MaxValue = 1,
                            }
                        },
                        new SettingsSlider<float>
                        {
                            Icon = SegoeFluent.ArrowRotateClockwise,
                            Label = "Rotation",
                            Current = new BindableFloat
                            {
                                MinValue = 0,
                                MaxValue = 1,
                            }
                        },
                    },
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5, 0),
                    Children = new Drawable[]
                    {
                        new FluentButton
                        {
                            Text = "Adjust",
                            Width = 90,
                            Style = ButtonStyle.Primary,
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Action = () => { },
                        },
                        new FluentButton
                        {
                            Text = "Reset",
                            Width = 90,
                            Style = ButtonStyle.Text,
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Action = () => { },
                        },
                    },
                },
            };

        private class PositionItem : SettingsItem
        {
            [BackgroundDependencyLoader]
            private void load()
            {
                Icon = SegoeFluent.ArrowMove;
                Label = "Position";

                LabelContainer.Add(new ThemableSpriteText
                {
                    Text = "0,0",
                    Font = SegoeUI.SemiBold.With(size: 16),
                    Colour = ThemeSlot.Black,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                });
            }
        }
    }
}
