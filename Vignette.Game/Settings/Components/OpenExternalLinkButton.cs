// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;
using System.IO;

namespace Vignette.Game.Settings.Components
{
    public class OpenExternalLinkButton : SettingsInteractableItem
    {
        private readonly string link;
        private readonly FileInfo file;
        private readonly Storage storage = null;

        protected virtual IconUsage? RightIcon => SegoeFluent.WindowNew;

        public OpenExternalLinkButton(Storage storage)
        {
            Action = () => storage.OpenInNativeExplorer();
        }

        public OpenExternalLinkButton(string link)
        {
            this.link = link;
        }

        public OpenExternalLinkButton(FileInfo file)
        {
            this.file = file;
        }

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            if (RightIcon.HasValue)
            {
                Foreground.Add(new ThemableSpriteIcon
                {
                    Size = new Vector2(12),
                    Icon = RightIcon.Value,
                    Colour = ThemeSlot.Black,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Margin = new MarginPadding { Right = 4 },
                });
            }

            if (storage != null)
                return;

            if (!string.IsNullOrEmpty(link) && Uri.IsWellFormedUriString(link, UriKind.Absolute))
            {
                if (link.StartsWith("https://") || link.StartsWith("http://"))
                    Action = () => host.OpenUrlExternally(link);
            }

            if (file != null)
            {
                Action = () => host.OpenFileExternally(file.FullName);
            }
        }
    }
}
