// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneButtons : UserInterfaceTestScene
    {
        public TestSceneButtons()
        {
            AddRange(new Drawable[]
            {
                createButton(ButtonStyle.Primary, hasIcon: false),
                createButton(ButtonStyle.Primary, hasText: false),
                createButton(ButtonStyle.Primary, isEnabled: false),
                createButton(ButtonStyle.Primary),
                createButton(ButtonStyle.Secondary, hasIcon: false),
                createButton(ButtonStyle.Secondary, hasText: false),
                createButton(ButtonStyle.Secondary, isEnabled: false),
                createButton(ButtonStyle.Secondary),
                createButton(ButtonStyle.Text, hasIcon: false),
                createButton(ButtonStyle.Text, hasText: false),
                createButton(ButtonStyle.Text, isEnabled: false),
                createButton(ButtonStyle.Text),
            });
        }

        private FluentButton createButton(ButtonStyle style, bool isEnabled = true, bool hasIcon = true, bool hasText = true)
        {
            var button = new FluentButton
            {
                Style = style,
                AutoSizeAxes = Axes.X,
            };

            button.Enabled.Value = isEnabled;

            if (hasText)
                button.Text = "Button";

            if (hasIcon)
                button.Icon = SegoeFluent.Circle;

            return button;
        }
    }
}
