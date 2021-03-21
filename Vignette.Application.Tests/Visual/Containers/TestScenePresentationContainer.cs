// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Graphics.Containers;

namespace Vignette.Application.Tests.Visual.Containers
{
    public class TestScenePresentationContainer : TestScene
    {
        private readonly Presentation<TestPresentableBox> boxes;

        private TestPresentableBox red;

        private TestPresentableBox green;

        private TestPresentableBox blue;

        public TestScenePresentationContainer()
        {
            Add(boxes = new Presentation<TestPresentableBox>
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(256),
            });
        }

        private TestPresentableBox createRedBox() => red = new TestPresentableBox(Colour4.Red);

        private TestPresentableBox createGreenBox() => green = new TestPresentableBox(Colour4.Green);

        private TestPresentableBox createBlueBox() => blue = new TestPresentableBox(Colour4.Blue);

        private void setupState(bool wrap = false)
        {
            AddStep("initialize", () =>
            {
                boxes.CanWrap = wrap;
                boxes.Clear();
                boxes.AddRange(new[]
                {
                    createRedBox(),
                    createGreenBox(),
                    createBlueBox(),
                });
            });
        }

        [TestCase]
        public void TestSlideCreation()
        {
            AddAssert("check if null is current", () => boxes.Current.Value == null);
            AddStep("create red box", () => boxes.Add(createRedBox()));
            AddAssert("check if red is current", () => boxes.Current.Value == red);
            AddStep("remove red box", () => boxes.Remove(red));
            AddAssert("check if null is current", () => boxes.Current.Value == null);
        }

        [TestCase]
        public void TestSlideInitialization()
        {
            setupState();
            AddAssert("check presence", () => red.IsPresent && !green.IsPresent && !blue.IsPresent);
            AddAssert("check if red is current", () => boxes.Current.Value == red);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestSlideForward(bool wrap)
        {
            setupState(wrap);
            AddStep("step forward", () => boxes.Next());
            AddAssert("check if green is current", () => boxes.Current.Value == green);
            AddRepeatStep("step forward 2x", () => boxes.Next(), 2);

            if (wrap)
                AddAssert("check if red is current", () => boxes.Current.Value == red);
            else
                AddAssert("check if blue is current", () => boxes.Current.Value == blue);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestSlideBackward(bool wrap)
        {
            setupState(wrap);
            AddStep("move to last", () => boxes.Last());
            AddStep("step backward", () => boxes.Previous());
            AddAssert("check if green is current", () => boxes.Current.Value == green);
            AddRepeatStep("step backward 2x", () => boxes.Previous(), 2);

            if (wrap)
                AddAssert("check if blue is current", () => boxes.Current.Value == blue);
            else
                AddAssert("check if red is current", () => boxes.Current.Value == red);
        }

        [TestCase]
        public void TestSlideSelection()
        {
            setupState();
            AddStep("go to green slide", () => boxes.Select(green));
            AddAssert("check if green is current", () => boxes.Current.Value == green);
            AddStep("go to third slide", () => boxes.Select(2));
            AddAssert("check if blue is current", () => boxes.Current.Value == blue);
        }

        private class TestPresentableBox : PresentationSlide
        {
            public TestPresentableBox(Colour4 colour)
            {
                Colour = colour;
                RelativeSizeAxes = Axes.Both;

                Child = new Box { RelativeSizeAxes = Axes.Both };
            }

            public override void OnEntering() => this.FadeIn();

            public override void OnExiting() => this.FadeOut();
        }
    }
}
