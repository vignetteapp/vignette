// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Vignette.Game.IO;

namespace Vignette.Game.Tests.IO
{
    [TestFixture]
    public class MonitoredStorageTest
    {
        [Test]
        public void TestMonitorAddFile()
        {
            using (var storage = new TemporaryNativeStorage(Guid.NewGuid().ToString()))
            using (var monitor = new MonitoredStorage(storage))
            {
                int creates = 0;
                monitor.FileCreated += _ => creates++;

                string name = Guid.NewGuid().ToString();
                storage.GetStream(name, FileAccess.ReadWrite).Dispose();

                Thread.Sleep(10);

                Assert.IsTrue(monitor.Files.Contains(name));
                Assert.IsTrue(creates == 1);
            }
        }

        [Test]
        public void TestMonitorRemoveFile()
        {
            using (var storage = new TemporaryNativeStorage(Guid.NewGuid().ToString()))
            using (var monitor = new MonitoredStorage(storage))
            {
                int removes = 0;
                monitor.FileDeleted += _ => removes++;

                string name = Guid.NewGuid().ToString();
                storage.GetStream(name, FileAccess.ReadWrite).Dispose();

                Thread.Sleep(10);

                storage.Delete(name);

                Thread.Sleep(10);

                Assert.IsFalse(monitor.Files.Contains(name));
                Assert.IsTrue(removes == 1);
            }
        }

        [Test]
        public void TestMonitorRenameFile()
        {
            using (var storage = new TemporaryNativeStorage(Guid.NewGuid().ToString()))
            using (var monitor = new MonitoredStorage(storage))
            {
                int renames = 0;
                monitor.FileRenamed += (_, __) => renames++;

                string name = Guid.NewGuid().ToString();
                storage.GetStream(name, FileAccess.ReadWrite).Dispose();

                Thread.Sleep(10);

                string nameReplace = Guid.NewGuid().ToString();
                var fi = new FileInfo(storage.GetFullPath(name));
                fi.MoveTo(Path.Combine(storage.GetFullPath(string.Empty), nameReplace));

                Thread.Sleep(10);

                Assert.IsTrue(monitor.Files.Contains(nameReplace));
                Assert.IsTrue(renames == 1);
            }
        }

        [Test]
        public void TestMonitorUpdateFile()
        {
            using (var storage = new TemporaryNativeStorage(Guid.NewGuid().ToString()))
            using (var monitor = new MonitoredStorage(storage))
            {
                int updates = 0;
                monitor.FileUpdated += _ => updates++;

                string name = Guid.NewGuid().ToString();
                storage.GetStream(name, FileAccess.ReadWrite).Dispose();

                Thread.Sleep(10);

                using (var stream = storage.GetStream(name, FileAccess.Write, FileMode.Open))
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("Hello");
                    writer.Flush();
                }

                Thread.Sleep(10);

                Assert.IsTrue(updates == 1);
            }
        }
    }
}
