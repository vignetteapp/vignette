// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Configuration.Settings
{
    public class SettingNavigation : VisibilityContainer
    {
        public const float NAVBAR_WIDTH = 50;

        public event Action OnHome;

        public event Action<SettingSection> OnSection;

        private FillFlowContainer<VignetteButton> buttonFlow;

        public SettingNavigation(IEnumerable<SettingSection> sections)
        {
            Width = NAVBAR_WIDTH;
            RelativeSizeAxes = Axes.Y;

            Masking = true;
            EdgeEffect = VignetteStyle.ElevationOne;

            Children = new Drawable[]
            {
                new VignetteBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                buttonFlow = new FillFlowContainer<VignetteButton>
                {
                    Width = NAVBAR_WIDTH - 10,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Spacing = new Vector2(0, 5),
                    Padding = new MarginPadding { Vertical = 5 },
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Y,
                    Child = new BrandingButton()
                    {
                        Action = () => OnHome?.Invoke(),
                        Margin = new MarginPadding { Bottom = 15 },
                    },
                }
            };

            foreach (var section in sections)
            {
                buttonFlow.Add(new ButtonIcon
                {
                    Size = new Vector2(NAVBAR_WIDTH - 10),
                    Icon = section.Icon,
                    Style = ButtonStyle.Override,
                    Action = () => OnSection?.Invoke(section),
                    IconSize = new Vector2(0.6f),
                    IconSizing = Axes.Both,
                    LabelColour = ThemeColour.Black,
                    CornerRadius = VignetteStyle.CornerRadius * 4,
                });
            }
        }

        protected override void PopIn()
        {
            this.MoveToX(0, 200, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this.MoveToX(-NAVBAR_WIDTH, 200, Easing.OutQuint);
        }

        private class BrandingButton : VignetteButton
        {
            private Sprite branding;

            private Bindable<string> theme;

            public BrandingButton()
            {
                Size = new Vector2(NAVBAR_WIDTH - 10);
                Style = ButtonStyle.NoFill;
                CornerRadius = VignetteStyle.CornerRadius * 4;
            }

            [BackgroundDependencyLoader]
            private void load(ApplicationConfigManager appConfig, TextureStore textures, ThemeStore themes)
            {
                branding.Texture = textures.Get("branding-icon");
                theme = appConfig.GetBindable<string>(ApplicationConfig.Theme);
                theme.BindValueChanged(t => branding.Colour = themes?.Get(t.NewValue)?.Get(ThemeColour.Black) ?? Colour4.White, true);
            }

            protected override Drawable CreateLabel() => branding = new Sprite
            {
                Size = new Vector2(0.75f),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                FillMode = FillMode.Fit,
                RelativeSizeAxes = Axes.Both,
            };
        }
    }
}
