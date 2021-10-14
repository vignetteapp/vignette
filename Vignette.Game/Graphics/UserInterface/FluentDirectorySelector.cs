// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDirectorySelector : DirectorySelector
    {
        protected override ScrollContainer<Drawable> CreateScrollContainer()
            => new FluentScrollContainer<Drawable>();

        protected override DirectorySelectorBreadcrumbDisplay CreateBreadcrumb()
            => new FluentDirectorySelectorBreadcrumbDisplay();

        protected override DirectorySelectorDirectory CreateDirectoryItem(DirectoryInfo directory, string displayName = null)
            => new FluentDirectorySelectorDirectory(directory, displayName);

        protected override DirectorySelectorDirectory CreateParentDirectoryItem(DirectoryInfo directory)
            => new FluentDirectorySelectorParentDirectory(directory);
    }
}
