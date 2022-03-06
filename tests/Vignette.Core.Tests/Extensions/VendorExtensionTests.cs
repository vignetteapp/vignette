// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Linq;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using NUnit.Framework;
using Stride.Core;
using Vignette.Core.Extensions;

namespace Vignette.Core.Tests.Extensions
{
    public class VendorExtensionTests
    {
        private ExtensionSystem sys;
        private TestVendorExtension ext;
        private V8Runtime run;

        [SetUp]
        public void SetUp()
        {
            run = new V8Runtime();
            sys = new ExtensionSystem(new ServiceRegistry());
            ext = new TestVendorExtension(run);
        }

        [TearDown]
        public void TearDown()
        {
            ext?.Dispose();
            sys?.Dispose();
            run?.Dispose();
        }

        [Test]
        public void TestRegisterCommand()
        {
            ext.Code = @"import { vignette } from 'vignette';

vignette.extension.onActivate(() => {
    vignette.commands.register('testCommand', () => true);
});

";

            sys.Load(ext);
            Assert.That(ext.Channels.Count, Is.EqualTo(1));
            Assert.That(ext.Channels.First().Key, Is.EqualTo("testCommand"));
            Assert.That(ext.Channels.First().Value, Is.AssignableTo<ScriptObject>());
            Assert.That(ext.Dispatch(null, "testCommand"), Is.True);
        }

        [Test]
        public void TestDispatchCommand()
        {
            ext.Code = @"import { vignette } from 'vignette';

vignette.extension.onActivate(() => {
    vignette.commands.register('testCommand', () => vignette.commands.dispatch('Test:sayHello', [ 'World' ]));
});

";

            sys.Load(new TestHostExtension());
            sys.Load(ext);
            Assert.That(ext.Dispatch(null, "testCommand", "World"), Is.EqualTo("Hello World from Test"));
        }
    }
}
