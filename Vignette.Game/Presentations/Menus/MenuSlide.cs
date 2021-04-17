// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Presentations;

namespace Vignette.Game.Presentations.Menus
{
    public abstract class MenuSlide : Slide
    {
        public virtual Drawable CreateHeader() => null;

        public virtual Drawable CreateFooter() => null;

        public MenuSlide()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        public override void OnEntering(ISlide last)
        {
            this
                .ScaleTo(1.05f)
                .ScaleTo(1, 200, Easing.OutQuint)
                .FadeIn(200, Easing.OutQuint);
        }

        public override void OnExiting(ISlide next)
        {
            this
                .ScaleTo(0.95f, 200, Easing.OutQuint)
                .FadeOut(200, Easing.OutQuint);
        }
    }
}
