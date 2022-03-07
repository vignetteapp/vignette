// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;

namespace Vignette.Core.Extensions
{
    public interface IExtension : IEquatable<IExtension>
    {
        string Name { get; }
        string Author { get; }
        string Description { get; }
        string Identifier { get; }
        Version Version { get; }
    }
}
