// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;

namespace Vignette.Application.Graphics.Interface
{
    public class VignetteEnumDropdown<T> : VignetteDropdown<T>
        where T : struct, Enum
    {
        public VignetteEnumDropdown()
        {
            Items = (T[])Enum.GetValues(typeof(T));
        }
    }
}
