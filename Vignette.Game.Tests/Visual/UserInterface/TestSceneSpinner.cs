// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
