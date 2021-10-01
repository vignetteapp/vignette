// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using NUnit.Framework;
using System;

namespace Vignette.Live2D.Tests.Cubism
{
    public class CubismCoreTest
    {
        [Test]
        public void TestCheckCoreVersion() => Assert.That(CubismCore.Version, Is.Not.EqualTo(default(CubismVersion)));

        [Test]
        public void TestCheckLogger()
        {
            Assert.That(CubismCore.GetLogger(), Is.Null);

            CubismCore.SetLogger(test_logger);

            Assert.That(CubismCore.GetLogger(), Is.EqualTo((CubismCore.CubismLogFunction)test_logger));

            static void test_logger(string message) => Console.WriteLine(message);
        }
    }
}
