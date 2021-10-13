// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Framework.Testing.Input;
using osuTK;
using osuTK.Graphics;
using Vignette.Game.Input;

namespace Vignette.Game.Tests.Visual
{
    public abstract class VignetteManualInputManagerTestScene : VignetteTestScene
    {
        protected override Container<Drawable> Content => content;

        private readonly Container content;

        protected virtual Vector2 InitialMousePosition => Vector2.Zero;

        protected ManualInputManager InputManager { get; }

        private readonly BasicButton buttonTest;

        private readonly BasicButton buttonLocal;

        protected VignetteManualInputManagerTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                InputManager = new ManualInputManager
                {
                    UseParentInput = true,
                    Child = new GlobalActionContainer(null)
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = content = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
                new Container
                {
                    Depth = float.MinValue,
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Margin = new MarginPadding(5),
                    CornerRadius = 5,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Color4.Black,
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.5f,
                        },
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Margin = new MarginPadding(5),
                            Spacing = new Vector2(5),
                            Children = new Drawable[]
                            {
                                new SpriteText
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    Text = "Input Priority"
                                },
                                new FillFlowContainer
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    Margin = new MarginPadding(5),
                                    Spacing = new Vector2(5),
                                    Direction = FillDirection.Horizontal,

                                    Children = new Drawable[]
                                    {
                                        buttonLocal = new BasicButton
                                        {
                                            Text = "local",
                                            Size = new Vector2(50, 30),
                                            Action = returnUserInput
                                        },
                                        buttonTest = new BasicButton
                                        {
                                            Text = "test",
                                            Size = new Vector2(50, 30),
                                            Action = returnTestInput
                                        },
                                    }
                                },
                            }
                        },
                    }
                },
            });
        }

        [SetUp]
        public void SetUp() => ResetInput();

        protected override void Update()
        {
            base.Update();

            buttonTest.Enabled.Value = InputManager.UseParentInput;
            buttonLocal.Enabled.Value = !InputManager.UseParentInput;
        }

        protected void ResetInput()
        {
            var currentState = InputManager.CurrentState;

            var mouse = currentState.Mouse;
            InputManager.MoveMouseTo(InitialMousePosition);
            mouse.Buttons.ForEach(InputManager.ReleaseButton);

            var keyboard = currentState.Keyboard;
            keyboard.Keys.ForEach(InputManager.ReleaseKey);

            var touch = currentState.Touch;
            touch.ActiveSources.ForEach(s => InputManager.EndTouch(new Touch(s, Vector2.Zero)));

            var joystick = currentState.Joystick;
            joystick.Buttons.ForEach(InputManager.ReleaseJoystickButton);

            ScheduleAfterChildren(returnUserInput);
        }

        private void returnUserInput() => InputManager.UseParentInput = true;

        private void returnTestInput() => InputManager.UseParentInput = false;
    }
}
