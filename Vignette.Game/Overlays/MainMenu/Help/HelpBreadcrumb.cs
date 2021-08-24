// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Linq;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Help
{
    public class HelpBreadcrumb : Breadcrumb<string>
    {
        public HelpBreadcrumb()
        {
            Current.ValueChanged += _ =>
            {
                foreach (var t in TabContainer.Children.OfType<BreadcrumbItem>().Where(t => t.State == Visibility.Hidden).ToArray())
                    RemoveItem(t.Value);
            };
        }
    }
}
