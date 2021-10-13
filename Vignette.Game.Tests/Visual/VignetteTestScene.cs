// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Testing;

namespace Vignette.Game.Tests.Visual
{
    public abstract class VignetteTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new VignetteTestSceneTestRunner();

        public class VignetteTestSceneTestRunner : VignetteGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
