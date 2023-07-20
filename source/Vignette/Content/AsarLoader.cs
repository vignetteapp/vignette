// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.IO;
using craftersmine.Asar.Net;

namespace Vignette.Content;
internal class AsarLoader : IContentLoader<AsarArchive>
{
    // while the library is capable of verifying if its a valid archive, we would like to do
    // it in the content loader level too so we can catch masquerading malicious files as we
    // load them.
    // The first 4 bytes of an asar archive is 04 00 00 00.
    private static readonly byte[] signature = new byte[] { 0x04, 0x00, 0x00, 0x00 };

    public AsarArchive Load(ReadOnlySpan<byte> bytes)
    {
        if (!MemoryExtensions.SequenceEqual(bytes[0..4], signature))
            throw new ArgumentException("Failed to find sequence \"04 00 00 00\" in byte sequence.", nameof(bytes));

        // read the bytes into a stream
        var stream = new MemoryStream(bytes.ToArray());
        return new AsarArchive(stream);
    }
}
