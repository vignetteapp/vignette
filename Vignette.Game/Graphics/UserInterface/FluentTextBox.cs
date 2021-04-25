// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
