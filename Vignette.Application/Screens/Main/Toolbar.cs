// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.Screens.Main.Sections;

namespace Vignette.Application.Screens.Main
{
    public class Toolbar : VisibilityContainer
    {
        public const float TOOLBAR_WIDTH = 40;

        public const float TOOLBAR_WIDTH_EXTENDED = 200;

        public const float TOOLBAR_WIDTH_CONTROLS = 440;

        private readonly NavigationBar navigationBar;

        private readonly Presentation<ToolbarSection> sections;

        public Toolbar()
        {
            Masking = true;
            RelativeSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                new Container
                {
                    Width = TOOLBAR_WIDTH_CONTROLS - TOOLBAR_WIDTH,
                    Margin = new MarginPadding { Left = TOOLBAR_WIDTH },
                    RelativeSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new ThemedSolidBox
                        {
                            ThemeColour = ThemeColour.NeutralLight,
                            RelativeSizeAxes = Axes.Both,
                        },
                        sections = new Presentation<ToolbarSection>
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    }
                },
                navigationBar = new NavigationBar
                {
                    ToggleRequested = () => ToggleVisibility(),
                    Items = new ToolbarSection[]
                    {
                        new BackgroundSettingSection(),
                        new CameraSettingSection(),
                        new ApplicationSettingSection(),
                    },
                },
            };

            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Colour4.Black.Opacity(0.4f),
                Radius = 20.0f,
                Hollow = true,
            };

            navigationBar.Current.BindValueChanged((e) =>
            {
                if (e.NewValue != null)
                {
                    if (!sections.Items.Contains(e.NewValue))
                    {
                        LoadComponentAsync(e.NewValue, loaded =>
                        {
                            sections.Add(loaded);
                            sections.Select(loaded);
                        });
                        return;
                    }

                    sections.Select(e.NewValue);
                }
            }, true);
        }

        protected override void PopIn()
        {
            this
                .MoveToX(0, 200, Easing.OutQuint)
                .FadeInFromZero(200, Easing.OutQuint)
                .ResizeWidthTo(TOOLBAR_WIDTH_CONTROLS, 200, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            // Masked composites for some reason bleed 1 pixel. We should account for that.
            this
                .MoveToX(-TOOLBAR_WIDTH - 1, 200, Easing.OutQuint)
                .FadeOutFromOne(200, Easing.OutQuint)
                .ResizeWidthTo(TOOLBAR_WIDTH, 200, Easing.OutQuint);
        }
    }
}
