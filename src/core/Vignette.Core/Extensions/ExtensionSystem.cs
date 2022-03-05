// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using Stride.Core;
using Stride.Core.Annotations;
using Stride.Games;
using Vignette.Core.Extensions.BuiltIns;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Extensions
{
    public class ExtensionSystem : GameSystemBase
    {
        public IReadOnlyCollection<IExtension> Loaded => extensions;
        private readonly HashSet<Extension> extensions = new HashSet<Extension>();

        public ExtensionSystem([NotNull] IServiceRegistry registry)
            : base(registry)
        {
        }

        public void Load(Extension extension)
        {
            if (Loaded.Contains(extension, EqualityComparer<IExtension>.Default))
                throw new ExtensionLoadException(@"Extension is already loaded.");

            if (extension is VendorExtension vendored)
            {
                var missing = vendored.Dependencies?.Where(dep => !Loaded.Any(ext => ext.Identifier == dep.Identifier && ext.Version <= dep.Version));

                if (missing.Any())
                    throw new ExtensionLoadException(@"Failed to load extension as one or more dependencies have not been found.");
            }

            if (extensions.Add(extension))
                extension.Activate(this);
        }

        public void Unload(Extension extension)
        {
            if (!Loaded.Contains(extension, EqualityComparer<IExtension>.Default))
                throw new ExtensionUnloadException(@"Extension is not loaded.");

            if (extension is VendorExtension vendored)
            {
                var allDependencies = Loaded.OfType<VendorExtension>().SelectMany(ext => ext.Dependencies).Distinct(EqualityComparer<VendorExtensionDependency>.Default);
                if (allDependencies.Any(dep => extension.Identifier == dep.Identifier && extension.Version <= dep.Version))
                    throw new ExtensionUnloadException(@"Failed to unload extension as one or more extensions depend on it.");
            }

            if (extensions.Remove(extension) && extension is not BuiltInExtension)
                extension.Deactivate();
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
