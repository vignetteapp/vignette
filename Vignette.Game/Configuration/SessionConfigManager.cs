// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;

namespace Vignette.Game.Configuration
{
    public class SessionConfigManager : InMemoryConfigManager<SessionSetting>
    {
        protected override void InitialiseDefaults() => Reset();

        public void Reset()
        {
            ensureDefault(SetDefault(SessionSetting.EditingBackground, false));
        }

        private static void ensureDefault<T>(Bindable<T> bindable) => bindable.SetDefault();
    }

    public enum SessionSetting
    {
        EditingBackground
    }
}
