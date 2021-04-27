// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Backgrounds;
using Vignette.Game.Screens.Backgrounds;

namespace Vignette.Game.Tests.Visual.Backgrounds
{
    public class TestSceneUserBackgrounds : VignetteTestScene
    {
        private readonly BackgroundScreen background;

        private Bindable<BackgroundType> type;

        private Bindable<string> asset;

        private Bindable<Colour4> colour;

        [Resolved]
        private VignetteConfigManager config { get; set; }

        public TestSceneUserBackgrounds()
        {
            Child = new BackgroundScreenStack(background = new BackgroundScreenUser());
        }

        [SetUp]
        public void SetUp() => Schedule(() =>
        {
            type = config.GetBindable<BackgroundType>(VignetteSetting.BackgroundType);
            asset = config.GetBindable<string>(VignetteSetting.BackgroundAsset);
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);
        });

        [TestCase(BackgroundType.Image)]
        [TestCase(BackgroundType.Video)]
        public void TestBackgroundType(BackgroundType t)
        {
            AddStep("set background type", () => type.Value = t);
        }
    }
}
