// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Graphics.Containers
{
    /// <summary>
    /// An abstract implementation of a view that is a child of <see cref="ViewContainer"/>.
    /// </summary>
    public abstract class View : VisibilityContainer, IView
    {
        protected override bool StartHidden => true;

        protected override void PopIn() => this.FadeIn();

        protected override void PopOut() => this.FadeOut();

        public virtual void OnExiting(IView last)
        {
        }

        public virtual void OnEntering(IView last)
        {
        }
    }
}
