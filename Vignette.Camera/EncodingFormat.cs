// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

namespace Vignette.Camera
{
    /// <summary>
    /// The file format used for encoding image data.
    /// </summary>
    public enum EncodingFormat
    {
        /// <summary>
        /// Windows Bitmap
        /// </summary>
        Bitmap,

        /// <summary>
        /// Sun Rasters
        /// </summary>
        Raster,

        /// <summary>
        /// Joint Photographic Experts Group
        /// </summary>
        JPEG,

        /// <summary>
        /// Joint Photographic Experts Group 2000
        /// </summary>
        JPEG2000,

        /// <summary>
        /// Tagged Image File Format
        /// </summary>
        TIFF,

        /// <summary>
        /// Portable Network Graphics
        /// </summary>
        PNG,

        /// <summary>
        /// Portable Image Format
        /// </summary>
        PBM,

        /// <summary>
        /// Web Photo
        /// </summary>
        WebP,
    }
}
