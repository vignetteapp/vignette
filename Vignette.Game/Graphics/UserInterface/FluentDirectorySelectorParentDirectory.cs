// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
