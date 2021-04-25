// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneTextBox : UserInterfaceTestScene
    {
        public TestSceneTextBox()
        {
            Add(new FluentTextBox
            {
                Width = 200,
                PlaceholderText = @"Bordered",
            });

            Add(new FluentTextBox
            {
                Width = 200,
                Style = TextBoxStyle.Underlined,
                PlaceholderText = @"Underlined",
            });

            Add(new FluentTextBox
            {
                Width = 200,
                Style = TextBoxStyle.Borderless,
                PlaceholderText = @"Borderless",
            });

            Add(new TestLetterOnlyTextBox
            {
                Width = 200,
                PlaceholderText = @"Letters Only",
            });

            Add(new FluentTextBox
            {
                Width = 200,
                ReadOnly = true,
                Text = @"Read Only",
            });

            Add(new FluentTextBox
            {
                Width = 200,
                PlaceholderText = @"Placeholder",
                Current = new Bindable<string>
                {
                    Disabled = true,
                },
            });

            Add(new FluentSearchBox
            {
                Width = 200,
            });

            Add(new FluentSearchBox
            {
                Width = 200,
                Style = TextBoxStyle.Underlined,
            });
        }

        private class TestLetterOnlyTextBox : FluentTextBox
        {
            protected override TextInputContainer CreateTextBox()
                => new LetterOnlyInputBox();

            private class LetterOnlyInputBox : TextInputContainer
            {
                protected override bool CanAddCharacter(char character) => char.IsLetter(character);
            }
        }
    }
}
