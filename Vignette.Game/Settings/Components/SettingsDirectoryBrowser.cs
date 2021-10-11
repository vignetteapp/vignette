// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsDirectoryBrowser : SettingsExpandedControl<FluentTextBox, string>, IHasPopover
    {
        protected override FluentTextBox CreateControl() => new FluentTextBox { RelativeSizeAxes = Axes.X, CharacterLimit = 10000 };

        private readonly Bindable<DirectoryInfo> currentDirectory = new Bindable<DirectoryInfo>();

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
                                Action = () => this.ShowPopover(),
                            }
                        }
                    }
                }
            });

            currentDirectory.ValueChanged += updateValue;
        }

        private void updateValue(ValueChangedEvent<DirectoryInfo> e)
        {
            if (RuntimeInfo.IsUnix && e.NewValue == null) //if we are on "Computer" on unix
            {
                Current.Value = "/";
            }
            else
            {
                Current.Value = e.NewValue.FullName;
            }
        }

        public Popover GetPopover() => new FluentPopover
        {
            Child = new FluentDirectorySelector()
            {
                Size = new Vector2(600, 200),
                CurrentPath = { BindTarget = currentDirectory },
            }
        };
    }
}
