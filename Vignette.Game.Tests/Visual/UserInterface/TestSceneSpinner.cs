// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osuTK;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneSpinner : UserInterfaceTestScene
    {
        public TestSceneSpinner()
        {
            Add(new Spinner { Size = new Vector2(64) });
        }
    }
}
