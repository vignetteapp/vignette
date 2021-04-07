// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using Vignette.Game.Bindables;

namespace Vignette.Game.Tests.Bindables
{
    [TestFixture]
    public class BindableDictionaryTest
    {
        private BindableDictionary<int, string> bindableStringDictionary;

        [SetUp]
        public void SetUp()
        {
            bindableStringDictionary = new BindableDictionary<int, string>();
        }

        #region Constructor

        [Test]
        public void TestConstructorDoesNotAddItemsByDefault()
        {
            Assert.IsEmpty(bindableStringDictionary);
        }

        [Test]
        public void TestConstructorWithItemsAddsItemsInternally()
        {
            var dictionary = new Dictionary<int, string>
            {
                { 0, "ok" },
                { 1, "nope" },
                { 2, "random" },
                { 3, null },
                { 4, "" }
            };

            var bindableDictionary = new BindableDictionary<int, string>(dictionary);

            Assert.Multiple(() =>
            {
                foreach (var item in dictionary)
                    Assert.Contains(item, bindableDictionary);

                Assert.AreEqual(dictionary.Count, bindableDictionary.Count);
            });
        }

        #endregion

        #region BindTarget

        /// <summary>
        /// Tests binding via the various <see cref="BindableDictionary{TKey, TValue}.BindTarget"/> methods.
        /// </summary>
        [Test]
        public void TestBindViaBindTarget()
        {
            BindableDictionary<int, string> parentBindable = new BindableDictionary<int, string>();

            BindableDictionary<int, string> bindable1 = new BindableDictionary<int, string>();
            IBindableDictionary<int, string> bindable2 = new BindableDictionary<int, string>();

            bindable1.BindTarget = parentBindable;
            bindable2.BindTarget = parentBindable;

            parentBindable.Add(5, "Test");

            Assert.That(bindable1[5], Is.EqualTo("Test"));
            Assert.That(bindable2[5], Is.EqualTo("Test"));
        }

        #endregion

        #region BindCollectionChanged

        [Test]
        public void TestBindCollectionChangedWithoutRunningImmediately()
        {
            var dictionary = new BindableDictionary<int, string> { { 1, "One" } };

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            dictionary.BindCollectionChanged((_, args) => triggeredArgs = args);

            Assert.That(triggeredArgs, Is.Null);
        }

        [Test]
        public void TestBindCollectionChangedWithRunImmediately()
        {
            var dictionary = new BindableDictionary<int, string>();

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            dictionary.BindCollectionChanged((_, args) => triggeredArgs = args, true);

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(triggeredArgs.NewItems, Is.EquivalentTo(dictionary));
        }

        #endregion

        #region dictionary[index]

        [Test]
        public void TestGetRetrievesObjectAtIndex()
        {
            bindableStringDictionary.Add(0, "0");
            bindableStringDictionary.Add(1, "1");
            bindableStringDictionary.Add(2, "2");

            Assert.AreEqual("1", bindableStringDictionary[1]);
        }

        [Test]
        public void TestSetMutatesObjectAtIndex()
        {
            bindableStringDictionary.Add(0, "0");
            bindableStringDictionary.Add(1, "1");
            bindableStringDictionary[1] = "2";

            Assert.AreEqual("2", bindableStringDictionary[1]);
        }

        [Test]
        public void TestGetWhileDisabledDoesNotThrowInvalidOperationException()
        {
            bindableStringDictionary.Add(0, "0");
            bindableStringDictionary.Disabled = true;

            Assert.AreEqual("0", bindableStringDictionary[0]);
        }

        [Test]
        public void TestSetWhileDisabledThrowsInvalidOperationException()
        {
            bindableStringDictionary.Add(0, "0");
            bindableStringDictionary.Disabled = true;

            Assert.Throws<InvalidOperationException>(() => bindableStringDictionary[0] = "1");
        }

        [Test]
        public void TestSetNotifiesSubscribers()
        {
            bindableStringDictionary.Add(0, "0");

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableStringDictionary[0] = "1";

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
            Assert.That(triggeredArgs.OldItems, Is.EquivalentTo(new KeyValuePair<int, string>(0, "0").Yield()));
            Assert.That(triggeredArgs.NewItems, Is.EquivalentTo(new KeyValuePair<int, string>(0, "1").Yield()));
            Assert.That(triggeredArgs.OldStartingIndex, Is.Zero);
            Assert.That(triggeredArgs.NewStartingIndex, Is.Zero);
        }

        [Test]
        public void TestSetNotifiesBoundLists()
        {
            bindableStringDictionary.Add(0, "0");

            var list = new BindableDictionary<int, string>();
            list.BindTo(bindableStringDictionary);

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            list.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableStringDictionary[0] = "1";

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
            Assert.That(triggeredArgs.OldItems, Is.EquivalentTo(new KeyValuePair<int, string>(0, "0").Yield()));
            Assert.That(triggeredArgs.NewItems, Is.EquivalentTo(new KeyValuePair<int, string>(0, "1").Yield()));
            Assert.That(triggeredArgs.OldStartingIndex, Is.Zero);
            Assert.That(triggeredArgs.NewStartingIndex, Is.Zero);
        }

        #endregion

        #region .Add(item)

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringAddsStringToEnumerator(string str)
        {
            var item = new KeyValuePair<int, string>(0, str);
            bindableStringDictionary.Add(item);

            Assert.Contains(item, bindableStringDictionary);
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringNotifiesSubscriber(string str)
        {
            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            var item = new KeyValuePair<int, string>(0, str);
            bindableStringDictionary.Add(item);

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(triggeredArgs.NewItems, Is.EquivalentTo(item.Yield()));
            Assert.That(triggeredArgs.NewStartingIndex, Is.Zero);
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringNotifiesSubscriberOnce(string str)
        {
            var triggeredArgs = new List<NotifyCollectionChangedEventArgs>();
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs.Add(args);

            bindableStringDictionary.Add(0, str);

            Assert.That(triggeredArgs, Has.Count.EqualTo(1));
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringNotifiesMultipleSubscribers(string str)
        {
            NotifyCollectionChangedEventArgs triggeredArgsA = null;
            NotifyCollectionChangedEventArgs triggeredArgsB = null;
            NotifyCollectionChangedEventArgs triggeredArgsC = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsA = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsB = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsC = args;

            bindableStringDictionary.Add(0, str);

            Assert.That(triggeredArgsA, Is.Not.Null);
            Assert.That(triggeredArgsB, Is.Not.Null);
            Assert.That(triggeredArgsC, Is.Not.Null);
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringNotifiesMultipleSubscribersOnlyAfterTheAdd(string str)
        {
            NotifyCollectionChangedEventArgs triggeredArgsA = null;
            NotifyCollectionChangedEventArgs triggeredArgsB = null;
            NotifyCollectionChangedEventArgs triggeredArgsC = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsA = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsB = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsC = args;

            Assert.That(triggeredArgsA, Is.Null);
            Assert.That(triggeredArgsB, Is.Null);
            Assert.That(triggeredArgsC, Is.Null);

            bindableStringDictionary.Add(0, str);
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringNotifiesBoundList(string str)
        {
            var list = new BindableDictionary<int, string>();
            list.BindTo(bindableStringDictionary);

            var item = new KeyValuePair<int, string>(0, str);
            bindableStringDictionary.Add(item);

            Assert.Contains(item, list);
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithStringNotifiesBoundLists(string str)
        {
            var listA = new BindableDictionary<int, string>();
            var listB = new BindableDictionary<int, string>();
            var listC = new BindableDictionary<int, string>();
            listA.BindTo(bindableStringDictionary);
            listB.BindTo(bindableStringDictionary);
            listC.BindTo(bindableStringDictionary);

            var item = new KeyValuePair<int, string>(0, str);
            bindableStringDictionary.Add(item);

            Assert.Contains(item, listA);
            Assert.Contains(item, listB);
            Assert.Contains(item, listC);
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithDisabledListThrowsInvalidOperationException(string str)
        {
            bindableStringDictionary.Disabled = true;

            Assert.Throws<InvalidOperationException>(() => { bindableStringDictionary.Add(0, str); });
        }

        [TestCase("a random string")]
        [TestCase("", Description = "Empty string")]
        [TestCase(null)]
        public void TestAddWithListContainingItemsDoesNotOverrideItems(string str)
        {
            var item = new KeyValuePair<int, string>(0, "existing string");
            bindableStringDictionary.Add(item);

            bindableStringDictionary.Add(1, str);

            Assert.Contains(item, bindableStringDictionary);
        }

        #endregion

        #region .Remove(item)

        [Test]
        public void TestRemoveWithDisabledListThrowsInvalidOperationException()
        {
            const int key = 0;
            const string item = "hi";
            bindableStringDictionary.Add(key, item);
            bindableStringDictionary.Disabled = true;

            Assert.Throws(typeof(InvalidOperationException), () => bindableStringDictionary.Remove(key));
        }

        [Test]
        public void TestRemoveWithAnItemThatIsNotInTheListReturnsFalse()
        {
            bool gotRemoved = bindableStringDictionary.Remove(0);

            Assert.IsFalse(gotRemoved);
        }

        [Test]
        public void TestRemoveWhenListIsDisabledThrowsInvalidOperationException()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);
            bindableStringDictionary.Disabled = true;

            Assert.Throws<InvalidOperationException>(() => { bindableStringDictionary.Remove(key); });
        }

        [Test]
        public void TestRemoveWithAnItemThatIsInTheListReturnsTrue()
        {
            const string item = "item";
            bindableStringDictionary.Add(0, item);

            bool gotRemoved = bindableStringDictionary.Remove(0);

            Assert.IsTrue(gotRemoved);
        }

        [Test]
        public void TestRemoveNotifiesSubscriber()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableStringDictionary.Remove(key);

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
            Assert.That(triggeredArgs.OldItems, Has.One.Items.EqualTo(new KeyValuePair<int, string>(key, item)));
            Assert.That(triggeredArgs.OldStartingIndex, Is.EqualTo(0));
        }

        [Test]
        public void TestRemoveNotifiesSubscriberWithCorrectReference()
        {
            var item = new TestAlwaysEqualModel();

            var bindableObjectList = new BindableList<TestAlwaysEqualModel> { item };

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableObjectList.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableObjectList.Remove(new TestAlwaysEqualModel());

            Assert.That(triggeredArgs.OldItems?[0] == item, Is.True);
        }

        [Test]
        public void TestRemoveDoesntNotifySubscribersOnNoOp()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);

            NotifyCollectionChangedEventArgs triggeredArgs = null;

            bindableStringDictionary.Remove(key);

            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableStringDictionary.Remove(key);

            Assert.That(triggeredArgs, Is.Null);
        }

        [Test]
        public void TestRemoveNotifiesSubscribers()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(0, item);

            NotifyCollectionChangedEventArgs triggeredArgsA = null;
            NotifyCollectionChangedEventArgs triggeredArgsB = null;
            NotifyCollectionChangedEventArgs triggeredArgsC = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsA = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsB = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsC = args;

            bindableStringDictionary.Remove(key);

            Assert.That(triggeredArgsA, Is.Not.Null);
            Assert.That(triggeredArgsB, Is.Not.Null);
            Assert.That(triggeredArgsC, Is.Not.Null);
        }

        [Test]
        public void TestRemoveNotifiesBoundList()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);
            var list = new BindableDictionary<int, string>();
            list.BindTo(bindableStringDictionary);

            bindableStringDictionary.Remove(key);

            Assert.IsEmpty(list);
        }

        [Test]
        public void TestRemoveNotifiesBoundLists()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);
            var listA = new BindableDictionary<int, string>();
            listA.BindTo(bindableStringDictionary);
            var listB = new BindableDictionary<int, string>();
            listB.BindTo(bindableStringDictionary);
            var listC = new BindableDictionary<int, string>();
            listC.BindTo(bindableStringDictionary);

            bindableStringDictionary.Remove(key);

            Assert.Multiple(() =>
            {
                Assert.False(listA.Contains(key));
                Assert.False(listB.Contains(key));
                Assert.False(listC.Contains(key));
            });
        }

        [Test]
        public void TestRemoveNotifiesBoundListSubscription()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);
            var list = new BindableDictionary<int, string>();
            list.BindTo(bindableStringDictionary);

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            list.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableStringDictionary.Remove(key);

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
            Assert.That(triggeredArgs.OldItems, Has.One.Items.EqualTo(new KeyValuePair<int, string>(key, item)));
            Assert.That(triggeredArgs.OldStartingIndex, Is.EqualTo(0));
        }

        [Test]
        public void TestRemoveNotifiesBoundListSubscriptions()
        {
            const int key = 0;
            const string item = "item";
            bindableStringDictionary.Add(key, item);
            var listA = new BindableDictionary<int, string>();
            listA.BindTo(bindableStringDictionary);

            NotifyCollectionChangedEventArgs triggeredArgsA1 = null;
            NotifyCollectionChangedEventArgs triggeredArgsA2 = null;
            listA.CollectionChanged += (_, args) => triggeredArgsA1 = args;
            listA.CollectionChanged += (_, args) => triggeredArgsA2 = args;

            var listB = new BindableDictionary<int, string>();
            listB.BindTo(bindableStringDictionary);

            NotifyCollectionChangedEventArgs triggeredArgsB1 = null;
            NotifyCollectionChangedEventArgs triggeredArgsB2 = null;
            listB.CollectionChanged += (_, args) => triggeredArgsB1 = args;
            listB.CollectionChanged += (_, args) => triggeredArgsB2 = args;

            bindableStringDictionary.Remove(key);

            Assert.That(triggeredArgsA1, Is.Not.Null);
            Assert.That(triggeredArgsA2, Is.Not.Null);
            Assert.That(triggeredArgsB1, Is.Not.Null);
            Assert.That(triggeredArgsB2, Is.Not.Null);
        }

        [Test]
        public void TestRemoveDoesNotNotifySubscribersBeforeItemIsRemoved()
        {
            const string item = "item";
            bindableStringDictionary.Add(0, item);

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            Assert.That(triggeredArgs, Is.Null);
        }

        #endregion

        #region .Clear()

        [Test]
        public void TestClear()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");

            bindableStringDictionary.Clear();

            Assert.IsEmpty(bindableStringDictionary);
        }

        [Test]
        public void TestClearWithDisabledListThrowsInvalidOperationException()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");
            bindableStringDictionary.Disabled = true;

            Assert.Throws(typeof(InvalidOperationException), () => bindableStringDictionary.Clear());
        }

        [Test]
        public void TestClearWithEmptyDisabledListThrowsInvalidOperationException()
        {
            bindableStringDictionary.Disabled = true;

            Assert.Throws(typeof(InvalidOperationException), () => bindableStringDictionary.Clear());
        }

        [Test]
        public void TestClearUpdatesCountProperty()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");

            bindableStringDictionary.Clear();

            Assert.AreEqual(0, bindableStringDictionary.Count);
        }

        [Test]
        public void TestClearNotifiesSubscriber()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            bindableStringDictionary.Clear();

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
            Assert.That(triggeredArgs.OldItems, Is.EquivalentTo(new Dictionary<int, string>
            {
                { 0, "testA" },
                { 1, "testA" },
                { 2, "testA" },
                { 3, "testA" },
                { 4, "testA" },
            }));
            Assert.That(triggeredArgs.OldStartingIndex, Is.EqualTo(0));
        }

        [Test]
        public void TestClearDoesNotNotifySubscriberBeforeClear()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgs = args;

            Assert.That(triggeredArgs, Is.Null);

            bindableStringDictionary.Clear();
        }

        [Test]
        public void TestClearNotifiesSubscribers()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");

            NotifyCollectionChangedEventArgs triggeredArgsA = null;
            NotifyCollectionChangedEventArgs triggeredArgsB = null;
            NotifyCollectionChangedEventArgs triggeredArgsC = null;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsA = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsB = args;
            bindableStringDictionary.CollectionChanged += (_, args) => triggeredArgsC = args;

            bindableStringDictionary.Clear();

            Assert.That(triggeredArgsA, Is.Not.Null);
            Assert.That(triggeredArgsB, Is.Not.Null);
            Assert.That(triggeredArgsC, Is.Not.Null);
        }

        [Test]
        public void TestClearNotifiesBoundBindable()
        {
            var bindableList = new BindableDictionary<int, string>();
            bindableList.BindTo(bindableStringDictionary);
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");
            for (int i = 0; i < 5; i++)
                bindableList.Add(i + 5, "testA");

            bindableStringDictionary.Clear();

            Assert.IsEmpty(bindableList);
        }

        [Test]
        public void TestClearNotifiesBoundBindables()
        {
            var bindableListA = new BindableDictionary<int, string>();
            bindableListA.BindTo(bindableStringDictionary);
            var bindableListB = new BindableDictionary<int, string>();
            bindableListB.BindTo(bindableStringDictionary);
            var bindableListC = new BindableDictionary<int, string>();
            bindableListC.BindTo(bindableStringDictionary);
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");
            for (int i = 0; i < 5; i++)
                bindableListA.Add(i + 5, "testA");
            for (int i = 0; i < 5; i++)
                bindableListB.Add(i + 10, "testA");
            for (int i = 0; i < 5; i++)
                bindableListC.Add(i + 15, "testA");

            bindableStringDictionary.Clear();

            Assert.Multiple(() =>
            {
                Assert.IsEmpty(bindableListA);
                Assert.IsEmpty(bindableListB);
                Assert.IsEmpty(bindableListC);
            });
        }

        [Test]
        public void TestClearDoesNotNotifyBoundBindablesBeforeClear()
        {
            var bindableListA = new BindableDictionary<int, string>();
            bindableListA.BindTo(bindableStringDictionary);
            var bindableListB = new BindableDictionary<int, string>();
            bindableListB.BindTo(bindableStringDictionary);
            var bindableListC = new BindableDictionary<int, string>();
            bindableListC.BindTo(bindableStringDictionary);
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, "testA");
            for (int i = 0; i < 5; i++)
                bindableListA.Add(i + 5, "testA");
            for (int i = 0; i < 5; i++)
                bindableListB.Add(i + 10, "testA");
            for (int i = 0; i < 5; i++)
                bindableListC.Add(i + 15, "testA");

            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(bindableListA);
                Assert.IsNotEmpty(bindableListB);
                Assert.IsNotEmpty(bindableListC);
            });

            bindableStringDictionary.Clear();
        }

        #endregion

        #region .CopyTo(array, index)

        [Test]
        public void TestCopyTo()
        {
            for (int i = 0; i < 5; i++)
                bindableStringDictionary.Add(i, $"test{i}");
            var dictionary = new KeyValuePair<int, string>[5];

            bindableStringDictionary.CopyTo(dictionary, 0);

            CollectionAssert.AreEquivalent(bindableStringDictionary, dictionary);
        }

        #endregion

        #region .Disabled

        [Test]
        public void TestDisabledWhenSetToTrueNotifiesSubscriber()
        {
            bool? isDisabled = null;
            bindableStringDictionary.DisabledChanged += b => isDisabled = b;

            bindableStringDictionary.Disabled = true;

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(isDisabled);
                Assert.IsTrue(isDisabled.Value);
            });
        }

        [Test]
        public void TestDisabledWhenSetToTrueNotifiesSubscribers()
        {
            bool? isDisabledA = null;
            bool? isDisabledB = null;
            bool? isDisabledC = null;
            bindableStringDictionary.DisabledChanged += b => isDisabledA = b;
            bindableStringDictionary.DisabledChanged += b => isDisabledB = b;
            bindableStringDictionary.DisabledChanged += b => isDisabledC = b;

            bindableStringDictionary.Disabled = true;

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(isDisabledA);
                Assert.IsTrue(isDisabledA.Value);
                Assert.IsNotNull(isDisabledB);
                Assert.IsTrue(isDisabledB.Value);
                Assert.IsNotNull(isDisabledC);
                Assert.IsTrue(isDisabledC.Value);
            });
        }

        [Test]
        public void TestDisabledWhenSetToCurrentValueDoesNotNotifySubscriber()
        {
            bindableStringDictionary.DisabledChanged += _ => Assert.Fail();

            bindableStringDictionary.Disabled = bindableStringDictionary.Disabled;
        }

        [Test]
        public void TestDisabledWhenSetToCurrentValueDoesNotNotifySubscribers()
        {
            bindableStringDictionary.DisabledChanged += _ => Assert.Fail();
            bindableStringDictionary.DisabledChanged += _ => Assert.Fail();
            bindableStringDictionary.DisabledChanged += _ => Assert.Fail();

            bindableStringDictionary.Disabled = bindableStringDictionary.Disabled;
        }

        [Test]
        public void TestDisabledNotifiesBoundLists()
        {
            var list = new BindableDictionary<int, string>();
            list.BindTo(bindableStringDictionary);

            bindableStringDictionary.Disabled = true;

            Assert.IsTrue(list.Disabled);
        }

        #endregion

        #region .GetEnumberator()

        [Test]
        public void TestGetEnumeratorDoesNotReturnNull()
        {
            Assert.NotNull(bindableStringDictionary.GetEnumerator());
        }

        [Test]
        public void TestGetEnumeratorWhenCopyConstructorIsUsedDoesNotReturnTheEnumeratorOfTheInputtedEnumerator()
        {
            var dictionary = new Dictionary<int, string> { { 0, "" } };
            var list = new BindableDictionary<int, string>(dictionary);

            var enumerator = list.GetEnumerator();

            Assert.AreNotEqual(dictionary.GetEnumerator(), enumerator);
        }

        #endregion

        #region .Description

        [Test]
        public void TestDescriptionWhenSetReturnsSetValue()
        {
            const string description = "The list used for testing.";

            bindableStringDictionary.Description = description;

            Assert.AreEqual(description, bindableStringDictionary.Description);
        }

        #endregion

        #region .Parse(obj)

        [Test]
        public void TestParseWithNullClearsList()
        {
            bindableStringDictionary.Add(0, "a item");

            bindableStringDictionary.Parse(null);

            Assert.IsEmpty(bindableStringDictionary);
        }

        [Test]
        public void TestParseWithArray()
        {
            var dictionary = new Dictionary<int, string> { { 0, "testA" }, { 1, "testB" } };

            bindableStringDictionary.Parse(dictionary);

            CollectionAssert.AreEquivalent(dictionary, bindableStringDictionary);
        }

        [Test]
        public void TestParseWithDisabledListThrowsInvalidOperationException()
        {
            bindableStringDictionary.Disabled = true;

            Assert.Multiple(() =>
            {
                Assert.Throws(typeof(InvalidOperationException), () => bindableStringDictionary.Parse(null));
                Assert.Throws(typeof(InvalidOperationException), () => bindableStringDictionary.Parse(new object[]
                {
                    "test", "test abc", "123"
                }));
            });
        }

        [Test]
        public void TestParseWithInvalidArgumentTypesThrowsArgumentException()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse(1));
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse(""));
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse(new object()));
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse(1.1));
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse(1.1f));
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse("test123"));
                Assert.Throws(typeof(ArgumentException), () => bindableStringDictionary.Parse(29387L));
            });
        }

        #endregion

        #region GetBoundCopy()

        [Test]
        public void TestBoundCopyWithAdd()
        {
            var boundCopy = bindableStringDictionary.GetBoundCopy();

            NotifyCollectionChangedEventArgs triggeredArgs = null;
            boundCopy.CollectionChanged += (_, args) => triggeredArgs = args;

            var item = new KeyValuePair<int, string>(0, "test");
            bindableStringDictionary.Add(item);

            Assert.That(triggeredArgs.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(triggeredArgs.NewItems, Is.EquivalentTo(item.Yield()));
            Assert.That(triggeredArgs.NewStartingIndex, Is.EqualTo(0));
        }

        #endregion

        private class TestAlwaysEqualModel : IEquatable<TestAlwaysEqualModel>
        {
            public bool Equals(TestAlwaysEqualModel other) => true;
        }
    }
}
