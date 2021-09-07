// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsFileBrowser : SettingsExpandedControl<FluentTextBox, string>
    {
        protected override FluentTextBox CreateControl() => new FluentTextBox { RelativeSizeAxes = Axes.X };

        protected override void InitializeControl()
        {
            Foreground.Add(new GridContainer
            {
                Height = 28,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                RelativeSizeAxes = Axes.X,
                ColumnDimensions = new[]
                {
                    new Dimension(),
                    new Dimension(GridSizeMode.Absolute, 100),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        Control = CreateControl(),
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding { Left = 10 },
                            Child = new FluentButton
                            {
                                Text = "Browse",
                                Style = ButtonStyle.Primary,
                                RelativeSizeAxes = Axes.X,
                                Action = () => { },
                            }
                        }
                    }
                }
            });
        }
    }
}
