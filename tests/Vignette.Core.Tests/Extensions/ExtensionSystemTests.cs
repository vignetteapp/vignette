// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using Microsoft.ClearScript;
using NUnit.Framework;
using Stride.Core;
using Vignette.Core.Extensions;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Tests.Extensions
{
    public class ExtensionSystemTests
    {
        private ExtensionSystem sys;

        [SetUp]
        public void SetUp()
        {
            sys = new ExtensionSystem(new ServiceRegistry());
        }

        [TearDown]
        public void TearDown()
        {
            sys?.Dispose();
        }

        [Test]
        public void TestLoading()
        {
            var a = new TestExtension(new VendorExtensionMetadata
            {
                Identifier = "a",
                Version = new Version("1.0.0"),
            });

            var b = new TestExtension(new VendorExtensionMetadata
            {
                Dependencies = new[]
                {
                    new VendorExtensionDependency { Identifier = "a", Version = new Version("0.0.1") }
                }
            });

            Assert.That(() => sys.Load(a), Throws.Nothing);
            Assert.That(() => sys.Load(b), Throws.Nothing);
            Assert.That(sys.Loaded.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestThrowDependencyMissing()
        {
            var ext = new TestExtension(new VendorExtensionMetadata
            {
                Dependencies = new[]
                {
                    new VendorExtensionDependency { Identifier = "test", Version = new Version("1.0.0") }
                }
            });

            Assert.That(() => sys.Load(ext), Throws.InstanceOf<ExtensionLoadException>());
        }

        [Test]
        public void TestThrowDependencyOlder()
        {
            var a = new TestExtension(new VendorExtensionMetadata
            {
                Identifier = "a",
                Version = new Version("1.0.0"),
            });

            var b = new TestExtension(new VendorExtensionMetadata
            {
                Dependencies = new[]
                {
                    new VendorExtensionDependency { Identifier = "a", Version = new Version("2.0.0") }
                }
            });

            Assert.That(() => sys.Load(a), Throws.Nothing);
            Assert.That(() => sys.Load(b), Throws.InstanceOf<ExtensionLoadException>());
        }

        private class TestExtension : VendorExtension
        {
            public TestExtension(VendorExtensionMetadata metadata)
                : base(metadata)
            {
            }

            protected override string GetDocumentContent(DocumentInfo documentInfo)
            {
                return @"export function activate() { }; export function deactivate() { };";
            }
        }
    }
}
