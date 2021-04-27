// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.IO;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace Vignette.Game.Tests.Visual
{
    public abstract class VignetteTestScene : TestScene
    {
        // protected Storage LocalStorage => new TemporaryNativeStorage(Path.Combine(RuntimeInfo.StartupDirectory, $"{GetType().Name}-{Guid.NewGuid()}"));

        // protected new DependencyContainer Dependencies { get; private set; }

        // protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        //     => Dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

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
