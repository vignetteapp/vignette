// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Settings;
using Vignette.Game.Settings.Components;
using Vignette.Game.Settings.Panels;
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
            AddStep("go to camera section", () => overlay.ScrollTo<RecognitionSection>());
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
            AddAssert("buttons enabled", () => overlay.NavigationButtonsEnabled == true);
        }

        [Test]
        public void TestButtonStateOnMainPanelState()
        {
            AddStep("show", () => overlay.Show());
            AddStep("hide body", () => overlay.HideBody());
            AddAssert("buttons disabled", () => overlay.NavigationButtonsEnabled == false);
            AddStep("hide", () => overlay.Hide());
        }

        [Test]
        public void TestSubPanelState()
        {
            AddStep("open submenu", () => overlay.ShowSubPanel(settingsSubPanel()));
            AddAssert("overlay is visible", () => overlay.State.Value == Visibility.Visible);
            AddAssert("buttons enabled", () => overlay.NavigationButtonsEnabled == true);
            AddStep("close submenu", () => overlay.Back());
            AddAssert("buttons enabled", () => overlay.NavigationButtonsEnabled == true);

            AddStep("open submenu", () => overlay.ShowSubPanel(settingsSubPanel()));
            AddStep("hide body", () => overlay.HideBody());
            AddStep("close submenu", () => overlay.Back());
            AddAssert("buttons disabled", () => overlay.NavigationButtonsEnabled == false);
            AddStep("hide", () => overlay.Hide());
        }

        private SettingsSubPanel settingsSubPanel() => new ConfirmationPanel("Test Message", overlay.Back);
    }
}
