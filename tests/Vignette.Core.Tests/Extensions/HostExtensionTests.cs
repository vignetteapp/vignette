// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using NUnit.Framework;
using Stride.Core;
using Vignette.Core.Extensions;

namespace Vignette.Core.Tests.Extensions
{
    public class BuiltInExtensionTests
    {
        private TestHostExtension ext;
        private ExtensionSystem sys;

        [SetUp]
        public void SetUp()
        {
            sys = new ExtensionSystem(new ServiceRegistry());
            ext = new TestHostExtension();
            sys.Load(ext);
        }

        [TearDown]
        public void TearDown()
        {
            sys.Dispose();
        }

        [Test]
        public void TestDispatch()
        {
            string result = ext.Dispatch<string>(null, "sayHello", "World");
            Assert.That(result, Is.EqualTo("Hello World from Test"));
        }

        [Test]
        public void TestDispatchInvalidChannel()
        {
            Assert.That(() => ext.Dispatch(null, "none"), Throws.InstanceOf<ChannelNotFoundException>());
        }

        [Test]
        public void TestDispatchInvalidParameters()
        {
            Assert.That(() => ext.Dispatch<string>(null, "sayHello", 42), Throws.InstanceOf<InvalidCastException>());
            Assert.That(() => ext.Dispatch<string>(null, "sayHello"), Throws.InstanceOf<IndexOutOfRangeException>());
        }
    }
}
