// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
