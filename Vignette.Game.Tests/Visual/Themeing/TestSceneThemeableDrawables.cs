// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual.Themeing
{
    public class TestSceneThemeableDrawables : ThemeProvidedTestScene
    {
        private ThemableBox themable;

        private Box target;

        [SetUp]
        public void SetUp() => Schedule(() =>
        {
            Clear();
            Provider.Current.Value = Theme.Light;
        });

        [Test]
        public void TestThemeSwitching()
        {
            AddStep("create themable", () => Schedule(() =>
            {
                Add(themable = new ThemableBox
                {
                    Size = new Vector2(100),
                    Colour = ThemeSlot.White,
                });
            }));

            AddAssert("is colour white", () => themable.Target.Colour == Colour4.White);
            AddStep("change theme", () => Provider.Current.Value = Theme.Dark);
            AddAssert("is colour black", () => themable.Target.Colour == Theme.Dark.Get(ThemeSlot.White));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestDrawableDetachment(bool expireTarget)
        {
            AddStep("create drawables", () => Schedule(() =>
            {
                Add(themable = new ThemableBox(false)
                {
                    Colour = ThemeSlot.White,
                });

                Add(target = themable.Create().With(d =>
                {
                    d.RelativeSizeAxes = Axes.None;
                    d.Size = new Vector2(100);
                }));
            }));

            AddAssert("is target white", () => target.Colour == Colour4.White);
            AddStep("change theme", () => Provider.Current.Value = Theme.Dark);
            AddAssert("is target black", () => target.Colour == Theme.Dark.Get(ThemeSlot.White));

            string toExpire = expireTarget ? "target" : "themable";
            string toCheck = expireTarget ? "themable" : "target";

            AddStep($"expire {toExpire}", () =>
            {
                if (expireTarget)
                    target.Expire();
                else
                    themable.Expire();
            });

            AddAssert($"is {toCheck} dead", () => expireTarget ? !themable.IsAlive : !target.IsAlive);
        }
    }
}
