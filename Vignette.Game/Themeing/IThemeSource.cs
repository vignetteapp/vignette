// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;

namespace Vignette.Game.Themeing
{
    public interface IThemeSource
    {
        event Action SourceChanged;

        Theme GetCurrent();
    }
}
