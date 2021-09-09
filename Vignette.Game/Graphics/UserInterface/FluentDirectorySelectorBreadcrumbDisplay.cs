// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDirectorySelectorBreadcrumbDisplay : DirectorySelectorBreadcrumbDisplay
    {
        protected override DirectorySelectorDirectory CreateDirectoryItem(DirectoryInfo directory, string displayName = null)
            => new FluentDirectorySelectorDirectoryItem(directory, displayName);

        protected override DirectorySelectorDirectory CreateRootDirectoryItem()
            => new FluentDirectorySelectorDirectoryRoot();

        public FluentDirectorySelectorBreadcrumbDisplay()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Margin = new MarginPadding { Bottom = 5 };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // There's no way to access the flow container!
            (InternalChild as FillFlowContainer).Padding = new MarginPadding(5);

            AddInternal(new ThemableEffectBox
            {
                Depth = 1,
                Colour = ThemeSlot.White,
                BorderColour = ThemeSlot.Gray110,
                BorderThickness = 1.5f,
                CornerRadius = 5,
                RelativeSizeAxes = Axes.Both,
            });
        }

        protected class FluentDirectorySelectorDirectoryItem : FluentDirectorySelectorDirectory
        {
            protected override IconUsage? DisplayIcon => null;

            public FluentDirectorySelectorDirectoryItem(DirectoryInfo directory, string displayName = null)
                : base(directory, displayName)
            {
            }

            [BackgroundDependencyLoader]
            private void load(Bindable<DirectoryInfo> currentDirectory)
            {
                if (Directory != null && currentDirectory.Value.Name == Directory.Name)
                    return;

                Flow.Add(new ThemableSpriteIcon
                {
                    Colour = ThemeSlot.Black,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Icon = SegoeFluent.ChevronRight,
                    Size = new Vector2(6),
                });
            }
        }

        protected class FluentDirectorySelectorDirectoryRoot : FluentDirectorySelectorDirectoryItem
        {
            public FluentDirectorySelectorDirectoryRoot()
                : base(null, "Computer")
            {
            }
        }
    }
}
