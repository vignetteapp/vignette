// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;

namespace Vignette.Game.Configuration
{
    public class SessionConfigManager : InMemoryConfigManager<SessionSetting>
    {
        protected override void InitialiseDefaults() => Reset();

        public void Reset()
        {
            ensureDefault(SetDefault(SessionSetting.EditingBackground, false));
            ensureDefault(SetDefault(SessionSetting.EditingAvatar, false));
        }

        private static void ensureDefault<T>(Bindable<T> bindable) => bindable.SetDefault();
    }

    public enum SessionSetting
    {
        EditingBackground,
        EditingAvatar,
    }
}
