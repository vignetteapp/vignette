// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentHexColourBox : FluentTextInput, IHasCurrentValue<Colour4>
    {
        private readonly BindableWithCurrent<Colour4> current = new BindableWithCurrent<Colour4>();

        public new Bindable<Colour4> Current
        {
            get => current.Current;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (!value.Disabled)
                    Input.Current.Value = value.Value.ToHex();

                current.Current = value;
            }
        }

        public FluentHexColourBox()
        {
            CommitOnFocusLost = true;

            AddInternal(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.X,
                            RelativeSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                new ThemableBox
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = ThemeSlot.Gray20,
                                },
                                new ThemableSpriteIcon
                                {
                                    Size = new Vector2(12),
                                    Icon = SegoeFluent.NumberSymbol,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Colour = ThemeSlot.Gray130,
                                    Margin = new MarginPadding { Horizontal = 10 },
                                },
                            },
                        },
                        Input,
                    }
                }
            });

            Current.BindValueChanged(e => Text = e.NewValue.ToHex(), true);
            Input.OnCommit += updateValue;
        }

        protected override TextInputContainer CreateTextBox() => new HexBox();

        private void updateValue(TextBox sender, bool newText)
        {
            try
            {
                current.Value = !string.IsNullOrEmpty(Text) ? Colour4.FromHex(Text) : Colour4.White;
            }
            catch (ArgumentException)
            {
                current.Value = Colour4.White;
            }
        }

        private class HexBox : TextInputContainer
        {
            public HexBox()
            {
                LengthLimit = 6;
            }

            protected override bool CanAddCharacter(char character) => Uri.IsHexDigit(character);

            protected override void OnTextCommitted(bool textChanged)
            {
                if (string.IsNullOrEmpty(Current.Value))
                    Current.Value = Colour4.White.ToHex();

                base.OnTextCommitted(textChanged);
            }
        }
    }
}
