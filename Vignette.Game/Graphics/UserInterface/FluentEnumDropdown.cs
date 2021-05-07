// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentEnumDropdown<T> : FluentDropdown<T>
        where T : struct, Enum
    {
        public FluentEnumDropdown()
        {
            Items = (T[])Enum.GetValues(typeof(T));
        }
    }
}
