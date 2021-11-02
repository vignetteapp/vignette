// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osu.Framework.Logging;
using osuTK;
using Vignette.Game.Extensions;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class FluentDropdown : FluentButtonBase
    {
        public FluentDropdownMenu Menu { get; private set; }

        protected new Bindable<bool> Enabled => base.Enabled;

        protected new Action Action
        {
            get => base.Action;
            set => base.Action = value;
        }

        public FluentDropdown()
        {
            Menu = CreateDropdownMenu();
        }

        protected abstract FluentDropdownMenu CreateDropdownMenu();
    }

    public class FluentDropdown<T> : FluentDropdown, IHasCurrentValue<T>
    {
        public Bindable<T> Current
        {
            get => bindableWithCurrent.Current;
            set => bindableWithCurrent.Current = value;
        }

        public IBindableList<T> ItemSource
        {
            get => itemSource;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (Items.Any())
                    throw new InvalidOperationException($"Cannot manually set {nameof(ItemSource)} when {nameof(Items)} is set.");

                if (boundItemSource != null)
                    itemSource.UnbindFrom(boundItemSource);

                itemSource.BindTo(boundItemSource = value);
            }
        }

        public IEnumerable<T> Items
        {
            get => itemMap.Keys;
            set
            {
                if (boundItemSource != null)
                    throw new InvalidOperationException($"Cannot manually set {nameof(Items)} when an {nameof(ItemSource)} is bound.");

                setItems(value);
            }
        }

        private readonly BindableWithCurrent<T> bindableWithCurrent = new BindableWithCurrent<T>();
        private readonly Dictionary<T, FluentMenuItem> itemMap = new Dictionary<T, FluentMenuItem>();
        private readonly IBindableList<T> itemSource = new BindableList<T>();
        private IBindableList<T> boundItemSource;

        private readonly ThemableSpriteText label;
        private readonly ThemableSpriteIcon icon;
        private readonly ThemableBox background;

        public FluentDropdown()
        {
            Height = 32;

            Masking = true;
            CornerRadius = 5.0f;

            InternalChildren = new Drawable[]
            {
                background = new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Left = 10, Right = 15 },
                    Children = new Drawable[]
                    {
                        label = new ThemableSpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Colour = ThemeSlot.Gray190,
                        },
                        icon = new ThemableSpriteIcon
                        {
                            Size = new Vector2(9),
                            Icon = SegoeFluent.ChevronDown,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Colour = ThemeSlot.Gray110,
                        }
                    }
                },
            };

            Action = handleClick;
            Current.BindDisabledChanged(d => Enabled.Value = !d, true);
            Current.BindValueChanged(_ => handleCurrentChange(), true);

            BackgroundResting = ThemeSlot.Gray20;
            BackgroundHovered = ThemeSlot.Gray10;
            BackgroundPressed = ThemeSlot.White;
            BackgroundDisabled = ThemeSlot.Gray60;
            LabelResting = ThemeSlot.Black;
            LabelDisabled = ThemeSlot.Black;

            ItemSource.CollectionChanged += (_, __) => setItems(ItemSource);
        }

        protected override FluentDropdownMenu CreateDropdownMenu() => new FluentDropdownMenu(this);

        protected override void UpdateBackground(ThemeSlot slot) => background.Colour = slot;

        protected override void UpdateLabel(ThemeSlot slot) => label.Colour = icon.Colour = slot;

        protected virtual LocalisableString GenerateItemText(T item)
        {
            switch (item)
            {
                case MenuItem m:
                    return m.Text.Value;

                case IHasText t:
                    return t.Text;

                case Enum e:
                    return e.GetLocalisableDescription();

                default:
                    return item?.ToString() ?? "null";
            }
        }

        public void AddItem(T item) => AddItem(GenerateItemText(item), item);

        public void AddItem(LocalisableString text, T item)
        {
            if (boundItemSource != null)
                throw new InvalidOperationException($"Cannot manually add items when an {nameof(ItemSource)} is bound.");

            addItem(text, item);
        }

        public bool RemoveItem(T item)
        {
            if (boundItemSource != null)
                throw new InvalidOperationException($"Cannot manually remove items when an {nameof(ItemSource)} is bound.");

            return removeItem(item);
        }

        public void ClearItems()
        {
            if (boundItemSource != null)
                throw new InvalidOperationException($"Cannot manually clear items when an {nameof(ItemSource)} is bound.");

            clearItems();
        }

        private void setItems(IEnumerable<T> items)
        {
            clearItems();

            if (items == null)
                return;

            items.ForEach(i => addItem(GenerateItemText(i), i));

            Menu.Items = itemMap.Values.ToArray();

            if (Current.Disabled)
                return;

            if (Current.Value == null || !itemMap.ContainsKey(Current.Value))
                Current.Value = Items.FirstOrDefault();
            else
                Current.TriggerChange();
        }

        private void addItem(LocalisableString text, T item)
        {
            if (itemMap.ContainsKey(item))
                return;
            //throw new ArgumentException($"The item {item} already exists in this {nameof(FluentDropdown<T>)}.");

            var menuItem = new FluentMenuItem(text, () =>
            {
                if (Current.Disabled)
                    return;

                Current.Value = item;
            });

            itemMap.Add(item, menuItem);
            Menu.Add(menuItem);
        }

        private bool removeItem(T item)
        {
            if (item == null)
                return false;

            if (!itemMap.TryGetValue(item, out var menuItem))
                return false;

            itemMap.Remove(item);
            Menu.Remove(menuItem);

            return true;
        }

        private void clearItems()
        {
            itemMap.Clear();
            Menu.Clear();
        }

        private void handleCurrentChange()
        {
            if (Current.Disabled || Current.Value == null || !itemMap.TryGetValue(Current.Value, out var menuItem))
                return;

            label.Text = GenerateItemText(Current.Value);
            Menu.SelectItem(menuItem);
        }

        private void handleClick()
        {
            if (!Current.Disabled)
                this.Expand();
        }
    }

    public static class FluentDropdownV2Extensions
    {
        public static void Expand(this FluentDropdown dropdown) => setTargetOnNearestDropdown(dropdown, dropdown);

        public static void Collapse(this FluentDropdown dropdown) => setTargetOnNearestDropdown(dropdown, null);

        private static void setTargetOnNearestDropdown(Drawable origin, FluentDropdown dropdown)
        {
            var container = origin as FluentDropdownMenuContainer ?? origin.FindNearestParent<FluentDropdownMenuContainer>();

            if (container == null)
                return;

            container.SetTarget(dropdown);
        }
    }
}
