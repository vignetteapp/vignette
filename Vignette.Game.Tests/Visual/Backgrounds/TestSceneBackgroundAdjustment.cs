// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osuTK;
using osuTK.Input;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Backgrounds;

namespace Vignette.Game.Tests.Visual.Backgrounds
{
    public class TestSceneBackgroundAdjustment : VignetteManualInputManagerTestScene
    {
        private readonly BackgroundAdjustmentControl control;

        private Bindable<float> rotation;

        private Bindable<float> scale;

        private Bindable<Vector2> offset;

        [Resolved]
        private VignetteConfigManager config { get; set; }

        public TestSceneBackgroundAdjustment()
        {
            Add(control = new BackgroundAdjustmentControl());
        }

        [SetUp]
        public void Setup() => Schedule(() =>
        {
            rotation = config.GetBindable<float>(VignetteSetting.BackgroundRotation);
            scale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
            offset = config.GetBindable<Vector2>(VignetteSetting.BackgroundOffset);

            scale.SetDefault();
            offset.SetDefault();
            rotation.SetDefault();
        });

        [Test]
        public void TestBackgroundAdjustOffset()
        {
            resetAdjustments();
            AddStep("show controls", () => control.Alpha = 1);

            AddStep("move", () =>
            {
                InputManager.MoveMouseTo(control);
                InputManager.PressButton(MouseButton.Left);
                InputManager.MoveMouseTo(control, new Vector2(30));
                InputManager.ReleaseButton(MouseButton.Left);
            });

            AddAssert("has moved", () => offset.Value.X > 0);
        }

        [Test]
        public void TestBackgroundAdjustRotation()
        {
            resetAdjustments();
            AddStep("show controls", () => control.Alpha = 1);

            AddStep("rotate", () =>
            {
                InputManager.MoveMouseTo(control);
                InputManager.PressButton(MouseButton.Left);
                InputManager.PressKey(Key.ShiftLeft);
                InputManager.MoveMouseTo(control, new Vector2(30, 0));
                InputManager.ReleaseButton(MouseButton.Left);
                InputManager.ReleaseKey(Key.ShiftLeft);
            });

            AddAssert("has rotated", () => rotation.Value > 0);
        }

        [Test]
        public void TestBackgroundAdjustScale()
        {
            resetAdjustments();
            AddStep("show controls", () => control.Alpha = 1);

            AddStep("scale", () =>
            {
                InputManager.MoveMouseTo(control);
                InputManager.ScrollVerticalBy(30);
            });

            AddAssert("has rescaled", () => scale.Value > 1.0f);
        }

        private void resetAdjustments() => AddStep("reset adjustments", () =>
        {
            scale.SetDefault();
            offset.SetDefault();
            rotation.SetDefault();
        });

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            config?.Dispose();
        }
    }
}
