// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Shapes;

namespace Vignette.Application.Tests.Visual.Interface
{
    public abstract class TestSceneInterface : TestScene
    {
        private readonly BindableBool darkMode = new BindableBool();

        private readonly Bindable<Colour4> accent = new Bindable<Colour4>(Colour4.White);

        private readonly FillFlowContainer content;

        [Resolved(CanBeNull = true)]
        private VignetteColour color { get; set; }

        public TestSceneInterface()
        {
            AddRange(new Drawable[]
            {
                new VignetteBox { RelativeSizeAxes = Axes.Both },
                content = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 10),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });
        }

        public void AddComponent(Drawable drawable) => content.Add(drawable);

        public void AddComponentRange(IEnumerable<Drawable> range) => content.AddRange(range);

        [Test]
        public void TestDarkMode()
        {
            AddToggleStep("toggle dark mode", state => darkMode.Value = state);
        }

        [Test]
        public void TestAccentColor()
        {
            AddSliderStep("red", 0.0f, 1.0f, 1.0f, c => accent.Value = new Colour4(c, accent.Value.G, accent.Value.B, 1.0f));
            AddSliderStep("blue", 0.0f, 1.0f, 1.0f, c => accent.Value = new Colour4(accent.Value.R, accent.Value.G, c, 1.0f));
            AddSliderStep("green", 0.0f, 1.0f, 1.0f, c => accent.Value = new Colour4(accent.Value.R, c, accent.Value.B, 1.0f));
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            color.Accent.BindTo(accent);
            color.DarkMode.BindTo(darkMode);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            color.Accent.UnbindFrom(accent);
            color.DarkMode.UnbindFrom(darkMode);
        }
    }
}
