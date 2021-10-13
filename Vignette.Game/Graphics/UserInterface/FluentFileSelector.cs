// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentFileSelector : FileSelector
    {
        public FluentFileSelector(string initialPath = null, string[] validFileExtensions = null)
            : base(initialPath, validFileExtensions)
        {
        }

        protected override ScrollContainer<Drawable> CreateScrollContainer()
            => new FluentScrollContainer<Drawable>();

        protected override DirectorySelectorBreadcrumbDisplay CreateBreadcrumb()
            => new FluentDirectorySelectorBreadcrumbDisplay();

        protected override DirectorySelectorDirectory CreateDirectoryItem(DirectoryInfo directory, string displayName = null)
            => new FluentDirectorySelectorDirectory(directory, displayName);

        protected override DirectorySelectorDirectory CreateParentDirectoryItem(DirectoryInfo directory)
            => new FluentDirectorySelectorParentDirectory(directory);

        protected override DirectoryListingFile CreateFileItem(FileInfo file)
            => new FluentDirectoryListingFile(file);

        private class FluentDirectoryListingFile : DirectoryListingFile
        {
            protected override IconUsage? Icon => null;

            public FluentDirectoryListingFile(FileInfo file)
                : base(file)
            {
            }

            protected override SpriteText CreateSpriteText() => new CheapThemableSpriteText { Colour = ThemeSlot.Black };

            protected override void LoadComplete()
            {
                base.LoadComplete();
                Flow.Margin = new MarginPadding();
                Flow.Insert(-1, new ThemableSpriteIcon
                {
                    Size = new Vector2(16),
                    Icon = SegoeFluent.Document,
                    Colour = ThemeSlot.Black,
                });
            }
        }
    }
}
