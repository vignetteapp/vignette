// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Backgrounds;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.IO;
using Vignette.Application.Screens.Main.Controls;

namespace Vignette.Application.Screens.Main.Sections
{
    public class BackgroundSettingSection : ToolbarSection
    {
        public override string Title => "Backgrounds";

        public override IconUsage Icon => FluentSystemIcons.Filled.ImageEdit24;

        private Bindable<BackgroundType> type;

        private Bindable<float> ox;

        private Bindable<float> oy;

        private Bindable<float> sx;

        private Bindable<string> imageConfig;

        private Bindable<string> videoConfig;

        private LabelledFileDropdown<Stream> videosDropdown;

        private LabelledFileDropdown<Texture> imagesDropdown;

        private readonly Bindable<string> inputOffsetX = new Bindable<string>();

        private readonly Bindable<string> inputOffsetY = new Bindable<string>();

        private readonly Bindable<string> inputScaleXY = new Bindable<string>();

        private FillFlowContainer offsetConfigContainer;

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager config, BackgroundImageStore images, BackgroundVideoStore videos)
        {
            type = config.GetBindable<BackgroundType>(ApplicationSetting.Background);
            ox = config.GetBindable<float>(ApplicationSetting.BackgroundOffsetX);
            oy = config.GetBindable<float>(ApplicationSetting.BackgroundOffsetY);
            sx = config.GetBindable<float>(ApplicationSetting.BackgroundScaleXY);
            imageConfig = config.GetBindable<string>(ApplicationSetting.BackgroundImageFile);
            videoConfig = config.GetBindable<string>(ApplicationSetting.BackgroundVideoFile);

            Children = new Drawable[]
            {
                new ThemedSpriteText
                {
                    Font = SegoeUI.Bold.With(size: 18),
                    Text = "Preview"
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    CornerRadius = 5.0f,
                    Masking = true,
                    Margin = new MarginPadding { Bottom = 10 },
                    Height = 250,
                    Width = 0.95f,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Colour4.Black,
                            RelativeSizeAxes = Axes.Both,
                        },
                        new Background(false)
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        },
                    },
                    EdgeEffect = new EdgeEffectParameters
                    {
                        Type = EdgeEffectType.Shadow,
                        Colour = Colour4.Black.Opacity(0.2f),
                        Radius = 10.0f,
                        Hollow = true,
                    },
                },
                new ThemedSpriteText
                {
                    Font = SegoeUI.Bold.With(size: 18),
                    Text = "Display"
                },
                new LabelledEnumDropdown<BackgroundType>
                {
                    Label = "Type",
                    Current = type,
                },
                imagesDropdown = new LabelledFileDropdown<Texture>
                {
                    Label = "Image",
                    ItemSource = images.Loaded,
                },
                videosDropdown = new LabelledFileDropdown<Stream>
                {
                    Label = "Video",
                    ItemSource = videos.Loaded,
                },
                new ThemedTextButton
                {
                    Text = "Open Folder",
                    Width = 200,
                    Action = () => images.OpenInNativeExplorer(),
                },
                offsetConfigContainer = new FillFlowContainer
                {
                    Spacing = new Vector2(0, 10),
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new ThemedSpriteText
                        {
                            Font = SegoeUI.Bold.With(size: 18),
                            Text = "Offset"
                        },
                        new FillFlowContainer
                        {
                            Alpha = type.Value != BackgroundType.Color ? 1 : 0,
                            Direction = FillDirection.Horizontal,
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Spacing = new Vector2(10, 0),
                            Children = new Drawable[]
                            {
                                new LabelledNumberBox
                                {
                                    Label = "X",
                                    Width = 50,
                                    Current = inputOffsetX,
                                },
                                new LabelledNumberBox
                                {
                                    Label = "Y",
                                    Width = 50,
                                    Current = inputOffsetY,
                                },
                                new LabelledNumberBox
                                {
                                    Label = "Scale",
                                    Width = 50,
                                    Current = inputScaleXY,
                                },
                                new ThemedIconButton
                                {
                                    Icon = FluentSystemIcons.Filled.ArrowUndo24,
                                    Size = new Vector2(25),
                                    Style = ButtonStyle.Override,
                                    Action = resetBackgroundOffsets,
                                    IconSize = new Vector2(15),
                                    LabelColour = ThemeColour.NeutralPrimary,
                                    Anchor = Anchor.BottomLeft,
                                    Origin = Anchor.BottomLeft,
                                },
                            }
                        },
                    }
                },
            };

            type.BindValueChanged(handleTypeChange, true);

            imagesDropdown.Current.ValueChanged += e => imageConfig.Value = e.NewValue?.Name ?? string.Empty;
            imagesDropdown.Current.Value = images.GetReference(imageConfig.Value);

            videosDropdown.Current.ValueChanged += e => videoConfig.Value = e.NewValue?.Name ?? string.Empty;
            videosDropdown.Current.Value = videos.GetReference(videoConfig.Value);

            inputOffsetX.ValueChanged += e =>
            {
                if (float.TryParse(e.NewValue, out float x))
                    ox.Value = x;
            };

            inputOffsetY.ValueChanged += e =>
            {
                if (float.TryParse(e.NewValue, out float y))
                    oy.Value = y;
            };

            inputScaleXY.ValueChanged += e =>
            {
                if (float.TryParse(e.NewValue, out float s))
                    sx.Value =  s;
            };

            ox.ValueChanged += e => inputOffsetX.Value = e.NewValue.ToString();
            oy.ValueChanged += e => inputOffsetY.Value = e.NewValue.ToString();
            sx.ValueChanged += e => inputScaleXY.Value = e.NewValue.ToString();
        }

        private void resetBackgroundOffsets()
        {
            ox.Value = oy.Value = 0.0f;
            sx.Value = 1.0f;
        }

        private void handleTypeChange(ValueChangedEvent<BackgroundType> e)
        {
            switch (e.NewValue)
            {
                case BackgroundType.Color:
                    imagesDropdown.FadeOut(200, Easing.OutQuint);
                    videosDropdown.FadeOut(200, Easing.OutQuint);
                    break;

                case BackgroundType.Image:
                    imagesDropdown.FadeIn(200, Easing.OutQuint);
                    videosDropdown.FadeOut(200, Easing.OutQuint);
                    break;

                case BackgroundType.Video:
                    imagesDropdown.FadeOut(200, Easing.OutQuint);
                    videosDropdown.FadeIn(200, Easing.OutQuint);
                    break;
            }

            offsetConfigContainer.FadeTo(e.NewValue != BackgroundType.Color ? 1 : 0, 200, Easing.OutQuint);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            inputOffsetX.Value = ox.Value.ToString();
            inputOffsetY.Value = oy.Value.ToString();
            inputScaleXY.Value = sx.Value.ToString();
        }
    }
}
