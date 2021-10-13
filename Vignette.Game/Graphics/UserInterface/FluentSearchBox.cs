// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A text input variant that shows a search icon and a button to clear text.
    /// </summary>
    public class FluentSearchBox : FluentTextInput
    {
        private readonly ThemableSpriteIcon searchIcon;

        public FluentSearchBox()
        {
            PlaceholderText = @"Search";

            AddInternal(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Children = new Drawable[]
                {
                    new Container
                    {
                        AutoSizeAxes = Axes.X,
                        RelativeSizeAxes = Axes.Y,
                        AutoSizeEasing = Easing.OutQuint,
                        AutoSizeDuration = 200,
                        Child = searchIcon = new ThemableSpriteIcon
                        {
                            Icon = SegoeFluent.Search,
                            Size = new Vector2(14),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Margin = new MarginPadding { Horizontal = 8 },
                        },
                    },
                    Input
                }
            });

            Current.ValueChanged += _ => updateState();
            Current.DisabledChanged += _ => updateState();

            updateState();
        }

        protected override void OnTextInputFocus()
        {
            searchIcon.Alpha = 0;
            base.OnTextInputFocus();
        }

        protected override void OnTextInputFocusLost()
        {
            searchIcon.Alpha = 1;
            base.OnTextInputFocusLost();
        }

        private void updateState()
        {
            searchIcon.Colour = !Current.Disabled ? ThemeSlot.Black : ThemeSlot.Gray90;
        }
    }
}
