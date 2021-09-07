// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Configuration;

namespace Vignette.Game.Screens.Stage
{
    public abstract class FileAssociatedBackground : CompositeDrawable
    {
        protected abstract IEnumerable<string> Extensions { get; }

        private Bindable<string> path;
        private Stream stream;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            path = config.GetBindable<string>(VignetteSetting.BackgroundPath);
            path.BindValueChanged(e => handlePathChange(), true);
        }

        protected abstract void OnFileChanged(Stream stream);

        private void handlePathChange()
        {
            performCleanup();

            if (!File.Exists(path.Value) && !Extensions.Contains(Path.GetExtension(path.Value)))
                return;

            stream = File.OpenRead(path.Value);
            OnFileChanged(stream);
        }

        protected override void Dispose(bool isDisposing)
        {
            if (stream != null)
                performCleanup();

            base.Dispose(isDisposing);
        }

        private void performCleanup()
        {
            stream?.Dispose();
            stream = null;
        }
    }
}
