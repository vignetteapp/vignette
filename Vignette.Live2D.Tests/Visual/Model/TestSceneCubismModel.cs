// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using NUnit.Framework;
using System.Linq;
using Vignette.Live2D.Model;

namespace Vignette.Live2D.Tests.Visual.Model
{
    public class TestSceneCubismModel : CubismModelTestScene
    {
        [Test]
        public void TestModelInitialization()
        {
            AddAssert("has drawables", () => Model.Drawables.Any());
            AddAssert("has parts", () => Model.Drawables.Any());
            AddAssert("has parameters", () => Model.Parameters.Any());
            AddAssert("check version", () => Model.Version != CubismMocVersion.csmMocVersion_Unknown);
        }

        [Test]
        public void TestModelParameterUpdate()
        {
            var target = Model.Parameters.FirstOrDefault(p => p.Name == "ParamMouthOpenY");
            AddAssert("check if default", () => target.Value == target.Default);
            AddStep("set to maximum", () => Schedule(() => target.Value = target.Maximum));
            AddWaitStep("wait", 5);
            AddAssert("check if maximum", () => target.Value == target.Maximum);
        }
    }
}
