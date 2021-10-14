// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

namespace Vignette.Game.Graphics.UserInterface
{
    /// <summary>
    /// A text input variant that displays a simple textbox.
    /// </summary>
    public class FluentTextBox : FluentTextInput
    {
        public FluentTextBox()
        {
            AddInternal(Input);
        }
    }
}
