// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using Vignette.Application.Graphics.Backgrounds;
using Vignette.Application.Input;

namespace Vignette.Application.Screens.Main
{
    public class MainMenu : Screen, IKeyBindingHandler<GlobalAction>
    {
        private Toolbar toolbar;

        private readonly BufferedContainer blurContainer;

        public MainMenu()
        {
            InternalChild = blurContainer = new BufferedContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new Background
                {
                    RelativeSizeAxes = Axes.Both
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            LoadComponentAsync(toolbar = new Toolbar(), AddInternal);
            toolbar.State.ValueChanged += e => blurContainer.BlurTo(e.NewValue == Visibility.Visible ? new Vector2(10) : Vector2.Zero, 500, Easing.OutQuint);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleToolbar:
                    toolbar?.ToggleVisibility();
                    return true;

                default:
                    return false;
            }
        }

        public void OnReleased(GlobalAction action)
        {
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeInFromZero(500, Easing.OutQuint);
        }

        protected override bool OnClick(ClickEvent e)
        {
            toolbar?.Hide();
            return base.OnClick(e);
        }
    }
}
