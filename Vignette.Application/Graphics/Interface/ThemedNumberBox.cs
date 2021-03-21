// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

namespace Vignette.Application.Graphics.Interface
{
    public class NumberBox : ThemedTextBox
    {
        protected override bool CanAddCharacter(char character) => char.IsNumber(character) || character == '.' || character == '-';
    }
}
