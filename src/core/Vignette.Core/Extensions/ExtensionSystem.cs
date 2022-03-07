// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ClearScript.V8;
using Stride.Core;
using Stride.Core.Annotations;
using Stride.Games;
using Vignette.Core.Extensions.Host;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Extensions
{
    public class ExtensionSystem : GameSystemBase
    {
        public readonly V8Runtime Runtime = new V8Runtime();
        public IReadOnlyCollection<IExtension> Loaded => extensions;
        private readonly HashSet<Extension> extensions = new HashSet<Extension>();

        public ExtensionSystem([NotNull] IServiceRegistry registry)
            : base(registry)
        {
        }

        public void Load(Extension extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            if (Loaded.Contains(extension, EqualityComparer<IExtension>.Default))
                throw new ExtensionLoadException(@"Extension is already loaded.");

            if (extension is VendorExtension vendored)
            {
                var missing = vendored.Dependencies?.Where(dep => !Loaded.Any(ext => ext.Identifier == dep.Identifier && ext.Version >= dep.Version)).Where(dep => dep.Required);

                if (missing.Any())
                    throw new ExtensionLoadException(@"Failed to load extension as one or more dependencies have not been met.");
            }

            if (extensions.Add(extension))
                extension.Activate(this);
        }

        public void Unload(Extension extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            if (!Loaded.Contains(extension, EqualityComparer<IExtension>.Default))
                throw new ExtensionUnloadException(@"Extension is not loaded.");

            if (extension is VendorExtension vendored)
            {
                var allDependencies = Loaded.OfType<VendorExtension>().SelectMany(ext => ext.Dependencies).Distinct(EqualityComparer<VendorExtensionDependency>.Default);
                if (allDependencies.Any(dep => extension.Identifier == dep.Identifier && dep.Required && extension.Version >= dep.Version))
                    throw new ExtensionUnloadException(@"Failed to unload extension as one or more extensions depend on it.");
            }

            if (extensions.Remove(extension) && extension is not HostExtension)
                extension.Deactivate();
        }

        protected override void Destroy()
        {
            base.Destroy();

            foreach (var ext in Loaded.OfType<VendorExtension>())
                ext.Dispose();

            Runtime.Dispose();
        }
    }

    public class ExtensionLoadException : Exception
    {
        public ExtensionLoadException(string message)
            : base(message)
        {
        }
    }

    public class ExtensionUnloadException : Exception
    {
        public ExtensionUnloadException(string message)
            : base(message)
        {
        }
    }
}
