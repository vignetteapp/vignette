// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Video;
using osuTK;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.IO;

namespace Vignette.Application.Graphics.Backgrounds
{
    public class BackgroundScreen : PresentationSlide
    {
        public BackgroundType Type { get; private set; }

        private readonly string asset;

        private Bindable<string> colour;

        private Bindable<float> ox;

        private Bindable<float> oy;

        private Bindable<float> sx;

        private bool adjust;

        private bool shouldAdjust => Type == BackgroundType.Color || !adjust;

        public BackgroundScreen(BackgroundType type, string asset = null, bool adjust = true)
        {
            Type = type;
            this.asset = asset;
            this.adjust = adjust;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager config, BackgroundImageStore images, BackgroundVideoStore videos)
        {
            ox = config.GetBindable<float>(ApplicationSetting.BackgroundOffsetX);
            ox.ValueChanged += _ => updatePosition();

            oy = config.GetBindable<float>(ApplicationSetting.BackgroundOffsetY);
            oy.ValueChanged += _ => updatePosition();

            sx = config.GetBindable<float>(ApplicationSetting.BackgroundScaleXY);
            sx.ValueChanged += _ => updatePosition();

            switch (Type)
            {
                case BackgroundType.Color:
                    AddInternal(new Box());
                    break;

                case BackgroundType.Image:
                    if (string.IsNullOrEmpty(asset))
                        return;

                    AddInternal(new Sprite { Texture = images.Get(asset), Size = Vector2.One, FillMode = FillMode.Fill });
                    break;

                case BackgroundType.Video:
                    if (string.IsNullOrEmpty(asset))
                        return;

                    AddInternal(new Video(videos.Get(asset)));
                    break;
            }

            InternalChild.RelativeSizeAxes = Axes.Both;

            colour = config.GetBindable<string>(ApplicationSetting.BackgroundColor);
            colour.BindValueChanged((e) => updateColour(), true);
        }

        private void updatePosition()
        {
            if (shouldAdjust)
                return;

            this.MoveTo(new Vector2(ox.Value, oy.Value), 200, Easing.OutQuint);
            this.ScaleTo(new Vector2(sx.Value), 200, Easing.OutQuint);
        }

        private void updateColour()
        {
            if (Type == BackgroundType.Color)
                Colour = Colour4.FromHex(colour.Value);
        }

        public override void OnEntering() => this.FadeInFromZero(200, Easing.OutQuint);

        public override void OnExiting() => this.FadeOutFromOne(200, Easing.OutQuint);

        protected override void LoadComplete()
        {
            base.LoadComplete();

            if (shouldAdjust)
                return;

            Position = new Vector2(ox.Value, oy.Value);
            Scale = new Vector2(sx.Value);
        }
    }
}
