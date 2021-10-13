// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;
using System.Linq;
using osu.Framework.IO.Stores;
using Vignette.Game.IO;
using Vignette.Live2D.Graphics;

namespace Vignette.Game.Screens.Stage
{
    public class CubismModelAvatar : CubismModel
    {
        private readonly RecursiveNativeStorage storage;

        public CubismModelAvatar(RecursiveNativeStorage storage)
            : base(new StorageBackedResourceStore(storage))
        {
            this.storage = storage;
        }

        protected override Stream GetModelSettingsStream()
        {
            try
            {
                string settingsFile = storage.GetFiles(".", "*.model3.json").FirstOrDefault();
                return storage.GetStream(settingsFile);
            }
            catch (Exception)
            {
                return Stream.Null;
            }
        }

        protected override Stream GetModelMocStream()
        {
            try
            {
                string mocFile = storage.GetFiles(".", "*.moc3").FirstOrDefault();
                return storage.GetStream(mocFile);
            }
            catch (Exception)
            {
                return Stream.Null;
            }
        }
    }
}
