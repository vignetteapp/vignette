// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
            string mocFile = storage.GetFiles(".", "*.moc3").FirstOrDefault();
            return storage.GetStream(mocFile);
        }
    }
}
