// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using Vignette.Game.Presentations;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestScenePresenation : TestScene
    {
        private Presentation presentation;

        private TestSlide red;

        private TestSlide blue;

        private TestSlide green;

        [SetUp]
        public void SetUp()
        {
            red = new TestSlide(Colour4.Red);
            blue = new TestSlide(Colour4.Blue);
            green = new TestSlide(Colour4.Green);

            presentation?.Expire();
            Add(presentation = new Presentation());
        }

        [Test]
        public void TestPushSlide()
        {
            AddStep("push slide", () => presentation.Push(red));
            AddAssert("slide is current", () => presentation.CurrentSlide == red);
        }

        [Test]
        public void TestSwitchingSlide()
        {
            AddStep("push red slide", () => presentation.Push(red));
            AddAssert("red slide is current", () => presentation.CurrentSlide == red);

            AddStep("push blue slide", () => presentation.Push(blue));
            AddAssert("blue slide is current", () => presentation.CurrentSlide == blue);

            AddStep("push green slide", () => presentation.Push(green));
            AddAssert("green slide is current", () => presentation.CurrentSlide == green);

            AddStep("push red slide", () => presentation.Push(red));
            AddAssert("red slide is current", () => presentation.CurrentSlide == red);
        }

        [Test]
        public void TestExitingSlide()
        {
            AddStep("push red slide", () => presentation.Push(red));
            AddStep("push blue slide", () => presentation.Push(blue));

            AddStep("push green slide", () => presentation.Push(green));
            AddAssert("green slide is current", () => presentation.CurrentSlide == green);

            AddStep("exit green slide", () => presentation.Exit());
            AddAssert("blue slide is current", () => presentation.CurrentSlide == blue);
        }

        private class TestSlide : Slide
        {
            public TestSlide(Colour4 colour)
            {
                InternalChild = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colour
                };
            }

            public override void OnEntering(ISlide last) => Show();

            public override void OnExiting(ISlide next) => Hide();
        }
    }
}
