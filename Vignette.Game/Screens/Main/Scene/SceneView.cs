// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Screens.Main.Menu;
using Vignette.Game.Screens.Main.Menu.Home;
using Vignette.Game.Screens.Main.Menu.Settings;

namespace Vignette.Game.Screens.Main.Scene
{
    public class SceneView : CompositeDrawable, IHasContextMenu
    {
        [Resolved]
        private MainMenu mainMenu { get; set; }

        private Box background;

        private Bindable<Colour4> colour;

        public SceneView()
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

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new FluentMenuItem("Home", () => mainMenu.SelectTab<HomePage>()),
            new FluentMenuItem("Configure Scene", () => mainMenu.SelectTab<SceneSettings>()),
        };
    }
}
