// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// The base class that handles all user text input. It handles styling the background and border but must be derived before use.
    /// See <see cref="FluentTextBox"/> for a basic use case.
    /// </summary>
    public abstract class FluentTextInput : CompositeDrawable, IHasCurrentValue<string>
    {
        private readonly ThemableMaskedBox background;

        private readonly ThemableMaskedBox border;

        protected TextInputContainer Input { get; private set; }

        public override bool HandlePositionalInput => true;

        private TextBoxStyle style;

        /// <summary>
        /// Gets or sets how this text input should be displayed. See <see cref="TextBoxStyle"/> for options.
        /// </summary>
        public TextBoxStyle Style
        {
            get => style;
            set
            {
                if (style == value)
                    return;

                style = value;
                Scheduler.AddOnce(updateStyle);
            }
        }

        /// <summary>
        /// Gets or sets whether the user is allowed to modify the contents of this text input.
        /// </summary>
        public bool ReadOnly
        {
            get => Input.ReadOnly;
            set => Input.ReadOnly = value;
        }

        /// <summary>
        /// Gets or sets the value of this text input.
        /// </summary>
        public string Text
        {
            get => Input.Text;
            set => Input.Text = value;
        }

        /// <summary>
        /// Gets or sets the placeholder text of this text input.
        /// </summary>
        public LocalisableString PlaceholderText
        {
            get => Input.PlaceholderText;
            set => Input.PlaceholderText = value;
        }

        /// <summary>
        /// Whether a commit should be triggered whenever the textbox loses focus.
        /// </summary>
        public bool CommitOnFocusLost
        {
            get => Input.CommitOnFocusLost;
            set => Input.CommitOnFocusLost = value;
        }

        /// <summary>
        /// Whether the textbox should rescind focus on commit.
        /// </summary>
        public bool ReleaseFocusOnCommit
        {
            get => Input.ReleaseFocusOnCommit;
            set => Input.ReleaseFocusOnCommit = value;
        }

        public Bindable<string> Current
        {
            get => Input.Current;
            set => Input.Current = value;
        }

        /// <summary>
        /// How long the input this text input is allowed to take.
        /// </summary>
        public int? CharacterLimit
        {
            get => Input.LengthLimit;
            set => Input.LengthLimit = value;
        }

        public FluentTextInput()
        {
            Height = 32;
            Masking = true;
            CornerRadius = 2.5f;

            Input = CreateTextBox().With(d =>
            {
                d.RelativeSizeAxes = Axes.Both;
                d.Masking = true;
                d.CornerRadius = 2.5f;
            });

            InternalChildren = new Drawable[]
            {
                background = new ThemableMaskedBox
                {
                    Depth = 1,
                    RelativeSizeAxes = Axes.Both,
                },
                border = new ThemableMaskedBox
                {
                    Depth = -1,
                }
            };

            Input.StateChange += OnTextInputStateChange;
            Input.FocusGained += OnTextInputFocus;
            Input.FocusLost += OnTextInputFocusLost;

            updateStyle();
        }

        protected virtual TextInputContainer CreateTextBox()
            => new TextInputContainer();

        protected virtual void OnTextInputStateChange(bool updateTextFlow, bool hasInvalidInput)
        {
            if (!Current.Disabled)
            {
                background.Colour = ThemeSlot.White;
                Input.Placeholder.Colour = ThemeSlot.Gray130;

                if (Style == TextBoxStyle.Bordered)
                {
                    if (!hasInvalidInput)
                    {
                        if (IsHovered && !inputHasFocus)
                            border.BorderColour = ThemeSlot.Gray160;
                        else if (inputHasFocus)
                            border.BorderColour = ThemeSlot.AccentPrimary;
                        else
                            border.BorderColour = ThemeSlot.Gray110;
                    }
                    else
                    {
                        border.BorderColour = ThemeSlot.Error;
                    }

                    border.BorderThickness = inputHasFocus ? 3.0f : 1.5f;
                }

                if (Style == TextBoxStyle.Underlined)
                {
                    if (!hasInvalidInput)
                    {
                        if (IsHovered && !inputHasFocus)
                            border.Colour = ThemeSlot.Gray160;
                        else if (inputHasFocus)
                            border.Colour = ThemeSlot.AccentPrimary;
                        else
                            border.Colour = ThemeSlot.Gray110;
                    }
                    else
                    {
                        border.Colour = ThemeSlot.Error;
                    }
                }
            }
            else
            {
                border.BorderThickness = 0;
                background.Colour = ThemeSlot.Gray30;
                Input.Placeholder.Colour = ThemeSlot.Gray90;
            }

            if (updateTextFlow)
            {
                foreach (var c in Input.TextFlow.Children.OfType<ThemableSpriteText>())
                    c.Colour = Current.Disabled ? ThemeSlot.Gray90 : ThemeSlot.Gray190;
            }
        }

        private bool inputHasFocus;

        protected virtual void OnTextInputFocus()
            => inputHasFocus = true;

        protected virtual void OnTextInputFocusLost()
            => inputHasFocus = false;

        private void updateStyle()
        {
            border.Show();

            switch (Style)
            {
                case TextBoxStyle.Bordered:
                    background.CornerRadius = 2.5f;
                    border.Height = 1;
                    border.Colour = ThemeSlot.Transparent;
                    border.CornerRadius = 2.5f;
                    border.RelativeSizeAxes = Axes.Both;
                    break;

                case TextBoxStyle.Underlined:
                    background.CornerRadius = 0;
                    border.CornerRadius = 0;
                    border.RelativeSizeAxes = Axes.X;
                    border.Anchor = Anchor.BottomCentre;
                    border.Origin = Anchor.BottomCentre;
                    border.Height = 1.5f;
                    border.BorderColour = ThemeSlot.Transparent;
                    break;

                case TextBoxStyle.Borderless:
                    border.Hide();
                    break;
            }

            OnTextInputStateChange(true, false);
        }

        /// <summary>
        /// The drawable responsible for handling and displaying the text flow and also handling the logic regarding input.
        /// </summary>
        protected class TextInputContainer : TextBox
        {
            public new ThemableSpriteText Placeholder;

            public new FillFlowContainer TextFlow => base.TextFlow;

            protected override float LeftRightPadding => 5;

            public event Action<bool, bool> StateChange;

            public event Action FocusGained;

            public event Action FocusLost;

            public TextInputContainer()
            {
                TextFlow.Height = 18;
                TextFlow.RelativeSizeAxes = Axes.None;

                Current.BindDisabledChanged(_ => updateState(true), true);
            }

            protected override Caret CreateCaret()
                => new FluentCaret { CaretWidth = 1 };

            protected override SpriteText CreatePlaceholder()
            {
                Placeholder = new ThemableSpriteText(false);
                Schedule(() => Add(Placeholder));

                return Placeholder.Create().With(d =>
                {
                    d.Font = SegoeUI.Regular;
                    d.Anchor = Anchor.CentreLeft;
                    d.Origin = Anchor.CentreLeft;
                });
            }

            protected override Drawable GetDrawableCharacter(char c) => new ThemableSpriteText
            {
                Text = c.ToString(),
                Font = SegoeUI.Regular.With(size: CalculatedTextSize),
                Colour = ThemeSlot.Gray190,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
            };

            private void updateState(bool updateTextFlow = false)
                => Scheduler.AddOnce(() => StateChange?.Invoke(updateTextFlow, hasInvalidInput));

            private bool hasInvalidInput;

            protected override void NotifyInputError()
            {
            }

            protected override bool OnClick(ClickEvent e) => true;

            protected override void OnUserTextAdded(string added)
            {
                hasInvalidInput = false;

                foreach (char c in added)
                    hasInvalidInput = !CanAddCharacter(c);

                updateState();
                base.OnUserTextAdded(added);
            }

            protected override void OnUserTextRemoved(string removed)
            {
                updateState();
                base.OnUserTextRemoved(removed);
            }

            protected override void OnFocus(FocusEvent e)
            {
                updateState();

                if (!Current.Disabled)
                    FocusGained?.Invoke();

                if (!ReadOnly && !Current.Disabled)
                    base.OnFocus(e);
            }

            protected override void OnFocusLost(FocusLostEvent e)
            {
                hasInvalidInput = false;
                updateState();

                if (!Current.Disabled)
                    FocusLost?.Invoke();

                if (!ReadOnly && !Current.Disabled)
                    base.OnFocusLost(e);
            }

            protected override bool OnHover(HoverEvent e)
            {
                if (Current.Disabled)
                    return false;

                updateState();

                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                if (Current.Disabled)
                    return;

                updateState();

                base.OnHoverLost(e);
            }

            protected class FluentCaret : Caret
            {
                public float CaretWidth { get; set; }

                private readonly ThemableBox cursor;

                private const float caret_move_time = 60;

                public FluentCaret()
                {
                    RelativeSizeAxes = Axes.Y;
                    Size = new Vector2(1, 0.6f);

                    Anchor = Anchor.CentreLeft;
                    Origin = Anchor.CentreLeft;

                    InternalChild = cursor = new ThemableBox
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = ThemeSlot.Black,
                    };
                }

                public override void DisplayAt(Vector2 position, float? selectionWidth)
                {
                    if (selectionWidth != null)
                    {
                        this.MoveTo(new Vector2(position.X, position.Y), 60, Easing.Out);
                        this.ResizeWidthTo(selectionWidth.Value + CaretWidth / 2, caret_move_time, Easing.Out);
                        cursor.Alpha = 1.0f;
                        cursor.Colour = ThemeSlot.AccentPrimary;
                        cursor.ClearTransforms();
                    }
                    else
                    {
                        this.MoveTo(new Vector2(position.X - CaretWidth / 2, position.Y), 60, Easing.Out);
                        this.ResizeWidthTo(CaretWidth, caret_move_time, Easing.Out);
                        cursor.Colour = ThemeSlot.Black;
                        cursor
                            .FadeOutFromOne(100, Easing.OutQuint)
                            .Delay(500)
                            .FadeInFromZero(100, Easing.OutQuint)
                            .Loop(500);
                    }
                }
            }
        }
    }

    public enum TextBoxStyle
    {
        Bordered,

        Underlined,

        Borderless,
    }
}
