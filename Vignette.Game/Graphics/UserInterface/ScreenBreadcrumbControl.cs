// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using System.Linq;

namespace Vignette.Game.Graphics.UserInterface
{
    public class ScreenBreadcrumbControl : BreadcrumbControl<IScreen>
    {
        public ScreenBreadcrumbControl(ScreenStack stack)
        {
            stack.ScreenPushed += screenPushed;
            stack.ScreenExited += screenExited;

            if (stack.CurrentScreen != null)
                screenPushed(null, stack.CurrentScreen);
        }

        protected override void SelectTab(TabItem<IScreen> tab)
            => tab.Value.MakeCurrent();

        private void screenPushed(IScreen lastScreen, IScreen newScreen)
        {
            AddItem(newScreen);
            Current.Value = newScreen;
        }

        private void screenExited(IScreen lastScreen, IScreen newScreen)
        {
            if (newScreen != null)
                Current.Value = newScreen;

            Items.ToList().SkipWhile(s => s != Current.Value).Skip(1).ForEach(RemoveItem);
        }
    }
}
