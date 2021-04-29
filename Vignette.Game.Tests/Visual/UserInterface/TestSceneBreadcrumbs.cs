// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneBreadcrumbs : UserInterfaceTestScene
    {
        private int count;

        public TestSceneBreadcrumbs()
        {
            TestBreadcrumb header;
            Add(header = new TestBreadcrumb
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

        private class TestBreadcrumb : Breadcrumb<string>
        {
            public TestBreadcrumb()
            {
                Current.ValueChanged += _ =>
                {
                    foreach (var t in TabContainer.Children.OfType<BreadcrumbItem>().Where(t => t.State == Visibility.Hidden).ToArray())
                        RemoveItem(t.Value);
                };
            }
        }
    }
}
