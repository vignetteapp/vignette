// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneBreadcrumbControl : UserInterfaceTestScene
    {
        public TestSceneBreadcrumbControl()
        {
            int count = 0;
            TestBreadcrumbControl header;

            Add(header = new TestBreadcrumbControl
            {
                RelativeSizeAxes = Axes.X,
            });

            AddStep("add", () =>
            {
                string name = $"step {count++}";
                header.AddItem(name);
                header.Current.Value = name;
            });
        }

        private class TestBreadcrumbControl : BreadcrumbControl<string>
        {
            public TestBreadcrumbControl()
            {
                Height = 36;
                Current.ValueChanged += _ =>
                {
                    foreach (var t in TabContainer.Children.OfType<BreadcrumbItem>().Where(t => t.State == Visibility.Hidden).ToArray())
                        RemoveItem(t.Value);
                };
            }
        }
    }
}
