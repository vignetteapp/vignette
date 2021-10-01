// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.IO;
using System.Runtime.InteropServices;
using Vignette.Live2D.Extensions;

namespace Vignette.Live2D.Model
{
    /// <summary>
    /// Represents a moc file.
    /// </summary>
    public class CubismMoc : IDisposable
    {
        private IntPtr buffer;

        /// <summary>
        /// The pointer for this moc file.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        /// <summary>
        /// The version of this moc file.
        /// </summary>
        public CubismMocVersion Version { get; private set; }

        public CubismMoc(byte[] moc)
        {
            initialize(moc);
        }

        public CubismMoc(Stream moc)
        {
            using var ms = new MemoryStream();
            moc.CopyTo(ms);
            initialize(ms.ToArray());
        }

        private void initialize(byte[] moc)
        {
            buffer = Marshal.AllocHGlobal(moc.Length + CubismCore.ALIGN_OF_MOC - 1);
            var aligned = buffer.Align(CubismCore.ALIGN_OF_MOC);

            Marshal.Copy(moc, 0, aligned, moc.Length);

            Handle = CubismCore.csmReviveMocInPlace(aligned, moc.Length);
            Version = CubismCore.csmGetMocVersion(aligned);

            if (Handle == IntPtr.Zero)
                throw new ArgumentException($"{nameof(moc)} is not a valid moc file.");

            if (Version > CubismCore.LatestMocVersion)
                throw new ArgumentException($"{nameof(moc)} has version '{Version}' while core can only support up to '{CubismCore.LatestMocVersion}'.");
        }

        #region Disposal

        public bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            Marshal.FreeHGlobal(buffer);

            IsDisposed = true;
        }

        ~CubismMoc()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
