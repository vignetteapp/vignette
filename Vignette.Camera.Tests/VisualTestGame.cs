// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace Vignette.Camera.Tests
{
    internal class VisualTestGame : Game
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new SafeAreaContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new DrawSizePreservingFillContainer
                {
                    Children = new Drawable[]
                    {
                        new TestBrowser("Vignette"),
                        new CursorContainer(),
                    },
                }
            };
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
