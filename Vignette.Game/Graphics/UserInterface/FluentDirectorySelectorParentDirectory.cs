// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using osu.Framework.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDirectorySelectorParentDirectory : FluentDirectorySelectorDirectory
    {
        protected override IconUsage? DisplayIcon => SegoeFluent.Folder;

        public FluentDirectorySelectorParentDirectory(DirectoryInfo directory)
            : base(directory, "..")
        {
        }
    }
}
