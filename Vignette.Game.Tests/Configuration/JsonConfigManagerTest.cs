// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using NUnit.Framework;
using osu.Framework.Platform;
using Vignette.Game.Configuration;

namespace Vignette.Game.Tests.Configuration
{
    [TestFixture]
    public class JsonConfigManagerTest
    {
        [Test]
        public void TestSaveLoad()
        {
            const int change = 727;
            using (var storage = new TemporaryNativeStorage(Guid.NewGuid().ToString()))
            {
                using (var manager = new TestConfigurationManager(storage))
                {
                    Assert.AreEqual(manager.Config.TestNumber, default(int));
                    manager.Config.TestNumber = change;
                    Assert.AreEqual(manager.Config.TestNumber, change);
                }

                using (var manager = new TestConfigurationManager(storage))
                    Assert.AreEqual(manager.Config.TestNumber, change);
            }
        }

        private class TestConfigurationManager : JsonConfigManager<TestConfiguration>
        {
            protected override string Filename => "test-config.json";

            public TestConfigurationManager(Storage storage)
                : base(storage)
            {
            }
        }

        [Serializable]
        private class TestConfiguration
        {
            public int TestNumber { get; set; }
        }
    }
}
