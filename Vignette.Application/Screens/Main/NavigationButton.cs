// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Screens.Main
{
    public abstract class NavigationButton : ThemedButton
    {
        protected Drawable SpriteText;

        protected Drawable SpriteIcon;

        public NavigationButton()
        {
            Size = new Vector2(200, 40);
            Style = ButtonStyle.Override;
            Enabled.Value = true;
        }

        protected abstract Drawable CreateIcon();

        protected abstract Drawable CreateText();

        protected override Drawable CreateLabel() => new Container
        {
            RelativeSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(Toolbar.TOOLBAR_WIDTH),
                    Child = SpriteIcon = CreateIcon().With(i =>
                    {
                        i.Anchor = Anchor.Centre;
                        i.Origin = Anchor.Centre;
                    }),
                },
                SpriteText = CreateText().With((System.Action<Drawable>)(s =>
                {
                    s.Anchor = Anchor.CentreLeft;
                    s.Origin = Anchor.CentreLeft;
                    s.Margin = new MarginPadding { Left = Toolbar.TOOLBAR_WIDTH + 5 };
                })),
            },
        };

        protected override bool OnClick(ClickEvent e)
        {
            Action?.Invoke();
            return false;
        }
    }
}
