// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Settings;
using Vignette.Game.Settings.Components;
using Vignette.Game.Settings.Sections;

namespace Vignette.Game.Tests.Visual
{
    public class TestSceneSettingsOverlay : VignetteTestScene
    {
        [Cached]
        private readonly SettingsOverlay overlay;

        public TestSceneSettingsOverlay()
        {
            Add(overlay = new SettingsOverlay());
            AddStep("toggle", () => overlay.ToggleVisibility());
        }

        [Test]
        public void TestJumpToSection()
        {
            AddStep("go to camera section", () => overlay.SelectTab<RecognitionSection>());
            AddAssert("overlay is visible", () => overlay.State.Value == Visibility.Visible);
            AddAssert("is camera section", () => overlay.CurrentSection.GetType().IsAssignableFrom(typeof(RecognitionSection)));
            AddStep("hide", () => overlay.Hide());
        }

        [Test]
        public void TestButtonStateOnVisibilityState()
        {
            AddStep("show", () => overlay.Show());
            AddStep("hide body", () => overlay.HideBody());
            AddStep("hide", () => overlay.Hide());
            AddStep("show", () => overlay.Show());
            AddAssert("buttons enabled", () => overlay.Buttons == true);
        }

        [Test]
        public void TestButtonStateOnMainPanelState()
        {
            AddStep("show", () => overlay.Show());
            AddStep("hide body", () => overlay.HideBody());
            AddAssert("buttons disabled", () => overlay.Buttons == false);
            AddStep("hide", () => overlay.Hide());
        }

        [Test]
        public void TestSubPanelState()
        {
            AddStep("open submenu", () => overlay.OpenSubPanel(new TestSubMenu()));
            AddAssert("overlay is visible", () => overlay.State.Value == Visibility.Visible);
            AddAssert("buttons disabled", () => overlay.Buttons == false);
            AddStep("close submenu", () => overlay.CloseSubPanel());
            AddAssert("buttons enabled", () => overlay.Buttons == true);

            AddStep("open submenu", () => overlay.OpenSubPanel(new TestSubMenu()));
            AddStep("hide body", () => overlay.HideBody());
            AddStep("close submenu", () => overlay.CloseSubPanel());
            AddAssert("buttons disabled", () => overlay.Buttons == false);
            AddStep("hide", () => overlay.Hide());
        }

        private class TestSubMenu : SettingsPanel
        {
            public TestSubMenu()
            {
                Children = new Drawable[]
                {
                    new SettingsSwitch
                    {
                        Label = "Test Switch"
                    },
                    new SettingsSwitch
                    {
                        Label = "Test Switch"
                    },
                    new SettingsSwitch
                    {
                        Label = "Test Switch"
                    },
                };
            }
        }
    }
}
