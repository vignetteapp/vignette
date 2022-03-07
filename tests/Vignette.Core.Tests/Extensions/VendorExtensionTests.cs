// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.ClearScript;
using NUnit.Framework;
using Stride.Core;
using Vignette.Core.Extensions;

namespace Vignette.Core.Tests.Extensions
{
    public class VendorExtensionTests
    {
        private ExtensionSystem sys;
        private TestVendorExtension ext;

        [SetUp]
        public void SetUp()
        {
            sys = new ExtensionSystem(new ServiceRegistry());
            ext = new TestVendorExtension();
        }

        [TearDown]
        public void TearDown()
        {
            ext?.Dispose();
            sys?.Dispose();
        }

        [Test]
        public void TestLoadSynchronous()
        {
            ext.Code = @"import { vignette } from 'vignette';

export function activate() { }

export function deactivate() { }

";

            Assert.That(() => sys.Load(ext), Throws.Nothing);
        }

        [Test]
        public void TestLoadAsynchronous()
        {
            ext.Code = @"import { vignette } from 'vignette';

export async function activate() { }

export async function deactivate() { }

";

            Assert.That(() => sys.Load(ext), Throws.Nothing);
        }

        [Test]
        public void TestRegisterCommand()
        {
            ext.Code = @"import { vignette } from 'vignette';

export function activate() {
    vignette.commands.register('testCommand', () => true);
}

export function deactivate() { }

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

export function activate() {
    vignette.commands.register('testCommand', () => vignette.commands.dispatch('Test:sayHello', [ 'World' ]));
};

export function deactivate() { }

";

            sys.Load(new TestHostExtension());
            sys.Load(ext);
            Assert.That(ext.Dispatch(null, "testCommand"), Is.EqualTo("Hello World from Test"));
        }

        [Test]
        public async Task TestDispatchCommandAsync()
        {
            ext.Code = @"import { vignette } from 'vignette';

export function activate() {
    vignette.commands.register('testCommand', async () => await vignette.commands.dispatchAsync('Test:sayHelloAsync', [ 'World' ]));
}

export function deactivate() { }

";

            sys.Load(new TestHostExtension());
            sys.Load(ext);
            Assert.That(await ext.DispatchAsync(null, "testCommand"), Is.EqualTo("Hello World from Test"));
        }
    }
}
