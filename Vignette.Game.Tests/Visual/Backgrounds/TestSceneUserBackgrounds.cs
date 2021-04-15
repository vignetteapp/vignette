// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;
using osuTK.Input;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Backgrounds;
using Vignette.Game.Screens;
using Vignette.Game.Screens.Backgrounds;

namespace Vignette.Game.Tests.Visual.Backgrounds
{
    public class TestSceneUserBackgrounds : ManualInputManagerTestScene
    {
        private readonly BackgroundScreen background;

        private readonly BackgroundAdjustmentControl control;

        private Bindable<BackgroundType> type;

        private Bindable<string> asset;

        private Bindable<float> rotation;

        private Bindable<float> scale;

        private Bindable<Vector2> offset;

        private Bindable<Colour4> colour;

        public TestSceneUserBackgrounds()
        {
            Children = new Drawable[]
            {
                new BackgroundScreenStack(background = new BackgroundScreenUser()),
                control = new BackgroundAdjustmentControl() { Alpha = 0 },
            };
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            type = config.GetBindable<BackgroundType>(VignetteSetting.BackgroundType);
            asset = config.GetBindable<string>(VignetteSetting.BackgroundAsset);
            rotation = config.GetBindable<float>(VignetteSetting.BackgroundRotation);
            scale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
            offset = config.GetBindable<Vector2>(VignetteSetting.BackgroundOffset);
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            resetAdjustments();
        }

        [TestCase(BackgroundType.Image)]
        [TestCase(BackgroundType.Video)]
        public void TestBackgroundType(BackgroundType t)
        {
            AddStep("hide controls", () => control.Alpha = 0);
            AddStep("set background type", () => type.Value = t);

            string file = null;
            switch (t)
            {
                case BackgroundType.Image:
                    file = "test-wallpaper.jpg";
                    break;

                case BackgroundType.Video:
                    file = "test-video.mp4";
                    break;
            }

            AddStep("clear asset", () => asset.Value = string.Empty);
            AddStep("set asset", () => asset.Value = file);
        }

        [Test]
        public void TestBackgroundAdjustOffset()
        {
            resetAdjustments();
            AddStep("show controls", () => control.Alpha = 1);

            AddStep("move", () =>
            {
                InputManager.MoveMouseTo(background);
                InputManager.PressButton(MouseButton.Left);
                InputManager.MoveMouseTo(background, new Vector2(30));
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
                InputManager.MoveMouseTo(background);
                InputManager.PressButton(MouseButton.Left);
                InputManager.PressKey(Key.ShiftLeft);
                InputManager.MoveMouseTo(background, new Vector2(30, 0));
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
                InputManager.MoveMouseTo(background);
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
    }
}
