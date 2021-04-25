// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A text input variant that shows a search icon and a button to clear text.
    /// </summary>
    public class FluentSearchBox : FluentTextInput
    {
        private FluentButton clearButton;

        private ThemableSpriteIcon searchIcon;

        public FluentSearchBox()
        {
            PlaceholderText = @"Search";

            AddInternal(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.Distributed),
                    new Dimension(GridSizeMode.AutoSize),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.X,
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeEasing = Easing.OutQuint,
                            AutoSizeDuration = 200,
                            Child = searchIcon = new ThemableSpriteIcon
                            {
                                Margin = new MarginPadding(8),
                                Icon = FluentSystemIcons.Search16,
                                Size = new Vector2(16),
                            },
                        },
                        Input,
                        new Container
                        {
                            AutoSizeAxes = Axes.X,
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeEasing = Easing.OutQuint,
                            AutoSizeDuration = 200,
                            Child = clearButton = new FluentButton
                            {
                                Icon = FluentSystemIcons.Dismiss16,
                                Width = 32,
                                Style = ButtonStyle.Text,
                                Action = () => Current.Value = string.Empty,
                            },
                        },
                    },
                },
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
            clearButton.Alpha = !string.IsNullOrEmpty(Current.Value) ? 1 : 0;
            searchIcon.Colour = !Current.Disabled ? ThemeSlot.AccentPrimary : ThemeSlot.Gray90;
        }
    }
}
