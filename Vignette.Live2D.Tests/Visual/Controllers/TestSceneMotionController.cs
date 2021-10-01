// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Vignette.Live2D.Graphics.Controllers;
using Vignette.Live2D.Motion;

namespace Vignette.Live2D.Tests.Visual.Controllers
{
    public class TestSceneMotionController : CubismModelTestScene
    {
        private TestCubismMotionController controller;

        [Test]
        public void TestSingleEnqueue()
        {
            AddStep("queue motion", () => controller.Enqueue("Idle"));
            AddAssert("check if playing", () => controller.Current != null);
            AddStep("finish motion", () => controller.Stop());
            AddAssert("check if not playing", () => controller.Current == null);
        }

        [Test]
        public void TestMultiplieEnqueue()
        {
            AddRepeatStep("queue motion", () => controller.Enqueue("Idle"), 5);
            AddAssert("check if playing", () => controller.Current != null);
            AddAssert("check queue is not empty", () => controller.Queue.Any());
            AddStep("clear queue", () => controller.Stop());
            AddAssert("check if not playing", () => controller.Current == null);
            AddAssert("check queue is empty", () => !controller.Queue.Any());
        }

        protected override void ModelChanged()
        {
            base.ModelChanged();
            Model.Add(controller = new TestCubismMotionController());
        }

        private class TestCubismMotionController : CubismMotionController
        {
            public new Queue<CubismMotion> Queue => base.Queue;
        }
    }
}
