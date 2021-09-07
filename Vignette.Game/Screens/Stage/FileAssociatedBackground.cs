// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Configuration;
using Vignette.Game.IO;

namespace Vignette.Game.Screens.Stage
{
    public abstract class FileAssociatedBackground : CompositeDrawable, ICanAcceptFiles
    {
        public abstract IEnumerable<string> Extensions { get; }

        private Bindable<string> path;
        private Stream stream;

        [Resolved]
        private VignetteGameBase game { get; set; }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            path = config.GetBindable<string>(VignetteSetting.BackgroundPath);
            path.BindValueChanged(e => handlePathChange(), true);
            game.RegisterFileHandler(this);
        }

        protected abstract void OnFileChanged(Stream stream);

        private void handlePathChange()
        {
            performCleanup();

            if (Extensions.Contains(Path.GetExtension(path.Value)) && File.Exists(path.Value))
            {
                stream = File.OpenRead(path.Value);
                OnFileChanged(stream);
            }
        }

        public void FileDropped(IEnumerable<string> files)
        {
            Schedule(() => path.Value = files.FirstOrDefault());
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            game.UnregisterFileHandler(this);

            if (stream != null)
                performCleanup();
        }

        private void performCleanup()
        {
            stream?.Dispose();
            stream = null;
        }
    }
}
