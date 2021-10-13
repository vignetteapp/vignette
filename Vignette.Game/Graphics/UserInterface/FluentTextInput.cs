// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// The base class that handles all user text input. It handles styling the background and border but must be derived before use.
    /// See <see cref="FluentTextBox"/> for a basic use case.
    /// </summary>
    public abstract class FluentTextInput : CompositeDrawable, IHasCurrentValue<string>
    {
        private readonly ThemableBox background;
        private readonly ThemableBox highlight;

        protected TextInputContainer Input { get; private set; }

        public override bool HandlePositionalInput => true;

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
            Height = 28;
            Masking = true;
            CornerRadius = 5;

            Input = CreateTextBox().With(d =>
            {
                d.RelativeSizeAxes = Axes.Both;
                d.Masking = true;
                d.CornerRadius = 2.5f;
            });

            InternalChildren = new Drawable[]
            {
                background = new ThemableBox
                {
                    Depth = 1,
                    RelativeSizeAxes = Axes.Both,
                },
                highlight = new ThemableBox
                {
                    Alpha = 0,
                    Depth = -1,
                    Height = 3,
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Colour = ThemeSlot.AccentPrimary,
                    RelativeSizeAxes = Axes.X,
                }
            };

            Input.StateChange += OnTextInputStateChange;
            Input.FocusGained += OnTextInputFocus;
            Input.FocusLost += OnTextInputFocusLost;
        }

        protected virtual TextInputContainer CreateTextBox() => new TextInputContainer();

        protected virtual void OnTextInputStateChange(bool updateTextFlow, bool hasInvalidInput)
        {
            if (!Current.Disabled)
            {
                background.Colour = ThemeSlot.Gray20;
                Input.Placeholder.Colour = ThemeSlot.Gray30;
            }
            else
            {
                background.Colour = ThemeSlot.Gray30;
                Input.Placeholder.Colour = ThemeSlot.Gray90;
            }

            if (updateTextFlow)
            {
                foreach (var c in Input.TextFlow.Children.OfType<ThemableSpriteText>())
                    c.Colour = Current.Disabled ? ThemeSlot.Gray90 : ThemeSlot.Black;
            }
        }

        protected virtual void OnTextInputFocus()
        {
            if (!ReadOnly)
                highlight.Alpha = 1;
        }

        protected virtual void OnTextInputFocusLost()
        {
            if (!ReadOnly)
                highlight.Alpha = 0;
        }

        /// <summary>
        /// The drawable responsible for handling and displaying the text flow and also handling the logic regarding input.
        /// </summary>
        protected class TextInputContainer : TextBox
        {
            public new CheapThemableSpriteText Placeholder => (CheapThemableSpriteText)base.Placeholder;

            public new FillFlowContainer TextFlow => base.TextFlow;

            protected override float LeftRightPadding => 5;

            public event Action<bool, bool> StateChange;

            public event Action FocusGained;

            public event Action FocusLost;

            public TextInputContainer()
            {
                TextFlow.Height = 14;
                TextFlow.RelativeSizeAxes = Axes.None;

                Current.BindDisabledChanged(_ => updateState(true), true);
            }

            protected override Caret CreateCaret() => new FluentCaret { CaretWidth = 1 };

            protected override SpriteText CreatePlaceholder() => new CheapThemableSpriteText
            {
                Font = SegoeUI.Regular,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
            };

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
}
