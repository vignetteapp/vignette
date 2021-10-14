// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
                    Assert.AreEqual(manager.TestNumber, default(int));
                    manager.TestNumber = change;
                    Assert.AreEqual(manager.TestNumber, change);
                }

                using (var manager = new TestConfigurationManager(storage))
                    Assert.AreEqual(manager.TestNumber, change);
            }
        }

        private class TestConfigurationManager : JsonConfigManager
        {
            protected override string Filename => "test-config.json";

            public int TestNumber { get; set; }

            public TestConfigurationManager(Storage storage)
                : base(storage)
            {
            }
        }
    }
}
