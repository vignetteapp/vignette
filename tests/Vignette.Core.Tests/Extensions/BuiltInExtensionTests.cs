// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Reflection;
using NUnit.Framework;
using Vignette.Core.Extensions;
using Vignette.Core.Extensions.BuiltIns;

namespace Vignette.Core.Tests.Extensions
{
    public class BuiltInExtensionTests
    {
        [Test]
        public void TestDispatch()
        {
            var ext = new TestExtension();
            ext.Activate(null);

            string result = ext.Dispatch<string>(null, "sayHello");
            Assert.That(result, Is.EqualTo("Hello World from Test"));
        }

        [Test]
        public void TestDispatchInvalidChannel()
        {
            var ext = new TestExtension();
            ext.Activate(null);

            Assert.That(() => ext.Dispatch(null, "none"), Throws.InstanceOf<ChannelNotFoundException>());
        }

        [Test]
        public void TestDispatchInvalidParameters()
        {
            var ext = new TestExtension();
            ext.Activate(null);

            Assert.That(() => ext.Dispatch<string>(null, "sayHello", "Hello"), Throws.InstanceOf<TargetParameterCountException>());
        }

        private class TestExtension : BuiltInExtension
        {
            public override string Name => @"Test";
            public override string Description => @"Test";
            public override string Identifier => @"Test";

            [Listen("sayHello")]
            public string SayHello()
            {
                return $"Hello World from {Name}";
            }
        }
    }
}
