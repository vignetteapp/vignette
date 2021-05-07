// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Input;
using Vignette.Game.Screens.Backgrounds;
using Vignette.Game.Screens.Menu;

namespace Vignette.Game.Screens.Scene
{
    public class SceneScreen : VignetteScreen, IHasContextMenu, IKeyBindingHandler<GlobalAction>
    {
        [Resolved(canBeNull: true)]
        private MainMenu menuScreen { get; set; }

        private Box background;

        private Bindable<Colour4> colour;

        public SceneScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            AddInternal(background = new Box
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
            });

            colour.BindValueChanged(e => background.Colour = e.NewValue, true);
        }

        protected override BackgroundScreen CreateBackground() => new BackgroundScreenTheme();

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new FluentMenuItem("Home", () => menuScreen?.SelectTab(typeof(HomeScreen))),
            new FluentMenuItem("Configure Scene", () => menuScreen?.SelectTab(typeof(SceneSettingScreen))),
            new FluentMenuDivider(),
            new FluentMenuItem("Help", () => menuScreen?.SelectTab(typeof(HelpScreen)), FluentSystemIcons.Book24),
        };

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            this
                .ScaleTo(1.1f, 250, Easing.OutBack)
                .FadeInFromZero(250, Easing.OutQuint);
        }

        public override bool OnExiting(IScreen next)
        {
            this
                .ScaleTo(1.0f, 250, Easing.OutBack)
                .FadeOutFromOne(250, Easing.OutQuint);

            return base.OnExiting(next);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleMainMenu:
                    menuScreen?.MakeCurrent();
                    return true;
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }
    }
}
