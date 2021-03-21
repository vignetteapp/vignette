// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Screens.Main;

namespace Vignette.Application.Screens.Intro
{
    public class Loader : Screen
    {
        private Screen mainMenu;

        private Screen splash;

        private LoadingSpinner spinner;

        private ScheduledDelegate showSpinner;

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            LoadComponentAsync(mainMenu = new MainMenu());

            LoadComponentAsync(splash = new Splash(mainMenu));

            LoadComponentAsync(spinner = new LoadingSpinner
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                Margin = new MarginPadding(20),
            }, _ =>
            {
                AddInternal(spinner);
                showSpinner = Scheduler.AddDelayed(spinner.Show, 200);
            });

            checkLoaded();
        }

        private void checkLoaded()
        {
            if (mainMenu.LoadState != LoadState.Ready && splash.LoadState != LoadState.Ready)
            {
                Schedule(checkLoaded);
                return;
            }

            showSpinner?.Cancel();
            spinner.Hide();

            Scheduler.AddDelayed(() => this.Push(splash), 5000);
        }

        private class LoadingSpinner : VisibilityContainer
        {
            private readonly SpriteIcon icon;

            public LoadingSpinner()
            {
                Size = new Vector2(32);
                Child = icon = new SpriteIcon
                {
                    Icon = FluentSystemIcons.Filled.SpinnerIos20,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                };
            }

            protected override void PopIn() => this.FadeInFromZero(500, Easing.OutQuint);

            protected override void PopOut() => this.FadeOutFromOne(500, Easing.OutQuint);

            protected override void LoadComplete()
            {
                base.LoadComplete();
                icon.RotateTo(0).Then()
                    .RotateTo(90, 200).Then()
                    .RotateTo(180, 200).Then()
                    .RotateTo(270, 200).Then()
                    .RotateTo(360, 200)
                    .Loop();
            }
        }
    }
}
