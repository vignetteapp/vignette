// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
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
    public class SettingsFileBrowser : SettingsExpandedControl<FluentTextBox, string>, IHasPopover
    {
        protected virtual string[] Extensions => null;

        protected override FluentTextBox CreateControl() => new FluentTextBox { RelativeSizeAxes = Axes.X };

        private readonly Bindable<FileInfo> currentDirectory = new Bindable<FileInfo>();

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

            currentDirectory.ValueChanged += e => Current.Value = e.NewValue.FullName;
        }

        public Popover GetPopover() => new FluentPopover
        {
            Child = new FluentFileSelector(!string.IsNullOrEmpty(Current.Value) ? Current.Value : null, Extensions)
            {
                Size = new Vector2(600, 200),
                CurrentFile = { BindTarget = currentDirectory },
            }
        };
    }
}
