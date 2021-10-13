// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDirectorySelectorDirectory : DirectorySelectorDirectory
    {
        protected override IconUsage? Icon => null;

        protected virtual IconUsage? DisplayIcon => (Directory != null && Directory.Parent == null) ? SegoeFluent.Storage : SegoeFluent.Folder;

        protected override SpriteText CreateSpriteText() => new CheapThemableSpriteText { Colour = ThemeSlot.Black };

        public FluentDirectorySelectorDirectory(DirectoryInfo directory, string displayName = null)
            : base(directory, displayName)
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Flow.Margin = new MarginPadding();

            if (!DisplayIcon.HasValue)
                return;

            Flow.Insert(-1, new ThemableSpriteIcon
            {
                Size = new Vector2(16),
                Icon = DisplayIcon.Value,
                Colour = ThemeSlot.Black,
            });
        }
    }
}
