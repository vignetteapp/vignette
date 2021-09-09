// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Extensions;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Graphics.Containers
{
    public class FluentDropdownMenuContainer : Container
    {
        private readonly Container content;
        private readonly Container menuContainer;
        private FluentDropdown target;

        protected override Container<Drawable> Content => content;

        public FluentDropdownMenuContainer()
        {
            InternalChildren = new Drawable[]
            {
                content = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                },
                menuContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }

        public void SetTarget(FluentDropdown newTarget)
        {
            if (newTarget == null || newTarget != target)
            {
                if (target != null)
                    target.Menu.State = MenuState.Closed;
            }

            target = newTarget;
            menuContainer.Clear(false);

            if (target == null)
                return;

            menuContainer.Add(target.Menu);

            target.Menu.Open();
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            if (target?.FindNearestParent<FluentDropdownMenuContainer>() != this || (!target?.IsPresent ?? false))
            {
                SetTarget(null);
                return;
            }

            if (target == null)
                return;

            target.Menu.Position = ToLocalSpace(target.ScreenSpaceDrawQuad).TopLeft - new Vector2(0, target.Menu.SelectedDrawableItem?.Y ?? 0);
        }
    }
}
