// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Text;
using Vignette.Graphics;

namespace Vignette.Content;

internal sealed class ShaderLoader : IContentLoader<ShaderMaterial>
{
    public ShaderMaterial Load(ReadOnlySpan<byte> bytes)
    {
        return ShaderMaterial.Create(Encoding.UTF8.GetString(bytes));
    }
}
