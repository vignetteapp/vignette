// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Linq;
using osu.Framework;
using osu.Framework.Input.Handlers.Mouse;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using Vignette.Live2D.Resources;

namespace Vignette.Live2D.Tests
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using var host = Host.GetSuitableHost("vignette");
            host.Run(new VisualTestGame());
        }

        private class VisualTestGame : Game
        {
            public VisualTestGame()
            {
                CubismCore.SetLogger(new CubismCore.CubismLogFunction(message => Logger.GetLogger("performance-cubism").Debug(message)));
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), "Resources"));

                var mouse = (MouseHandler)Host.AvailableInputHandlers.Single(i => i is MouseHandler);
                mouse.UseRelativeMode.Value = false;

                Add(new TestBrowser("Vignette"));
            }
        }
    }
}
