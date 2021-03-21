// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;

namespace Vignette.Application.Graphics.Interface
{
    public class ThemedEnumDropdown<T> : ThemedDropdown<T>
        where T : struct, Enum
    {
        public ThemedEnumDropdown()
        {
            Items = (T[])Enum.GetValues(typeof(T));
        }
    }
}
