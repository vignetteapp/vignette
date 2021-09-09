// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using Vignette.Game.Configuration.Converters;

namespace Vignette.Game.Graphics.Themeing
{
    /// <summary>
    /// Contains a dictionary of slots and colours and has the ability to retrieve colours back for use.
    /// </summary>
    [JsonConverter(typeof(ThemeConverter))]
    public partial class Theme
    {
        /// <summary>
        /// The name of this theme.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Invoked when a colour has been added or changed in this theme.
        /// </summary>
        public event Action ColoursChanged;

        private Dictionary<ThemeSlot, Colour4> colours;

        /// <summary>
        /// Create an empty theme.
        /// </summary>
        /// <param name="name">The name of this theme.</param>
        public Theme(string name)
        {
            Name = name;
            addConstants();
        }

        /// <summary>
        /// Create an unnamed empty theme.
        /// </summary>
        public Theme()
        {
            addConstants();
        }

        /// <summary>
        /// Sets a colour in the theme in the provided <see cref="ThemeSlot"/>.
        /// </summary>
        /// <param name="slot">The slot to add the colour to.</param>
        /// <param name="hex">The colour represented as a valid hex string.</param>
        /// <param name="replace">Should we overwrite if there is an existing color.</param>
        public Theme Set(ThemeSlot slot, string hex, bool replace = false)
            => Set(slot, Colour4.FromHex(hex), replace);

        /// <summary>
        /// Sets a colour in the theme in the provided <see cref="ThemeSlot"/>.
        /// </summary>
        /// <param name="slot">The slot to add the colour to.</param>
        /// <param name="colour">The colour to set the slot's value to</param>
        /// <param name="replace">Should we overwrite if there is an existing color.</param>
        public Theme Set(ThemeSlot slot, Colour4 colour, bool replace = false)
        {
            if (colours == null)
                colours = new Dictionary<ThemeSlot, Colour4>();

            if (!colours.ContainsKey(slot))
            {
                colours.Add(slot, colour);
            }
            else if (replace)
            {
                colours[slot] = colour;
            }

            ColoursChanged?.Invoke();
            return this;
        }

        /// <summary>
        /// Gets the colour of the specified <see cref="ThemeSlot"/>.
        /// </summary>
        /// <param name="slot">The colour slot to retrieve or the default colour if it doesn't exist.</param>
        public Colour4 Get(ThemeSlot slot)
        {
            if (colours.TryGetValue(slot, out Colour4 value))
                return value;

            return Colour4.White;
        }

        /// <summary>
        /// Serializes the current theme as a dictionary.
        /// </summary>
        public IDictionary<string, string> Serialize()
        {
            var dict = new Dictionary<string, string>();

            foreach ((var slot, var colour) in colours)
                dict.Add(slot.GetDescription(), colour.ToHex());

            return dict;
        }

        private void addConstants()
        {
            Set(ThemeSlot.Transparent, Colour4.Transparent);
            Set(ThemeSlot.Error, "A80000");
            Set(ThemeSlot.ErrorBackground, "FDE7E9");
            Set(ThemeSlot.Success, "107C10");
            Set(ThemeSlot.SuccessBackground, "DFF6DD");
            Set(ThemeSlot.SevereWarning, "D83B01");
            Set(ThemeSlot.SevereWarningBackground, "FED9CC");
            Set(ThemeSlot.Warning, "797775");
            Set(ThemeSlot.WarningBackground, "FFF4CE");
        }

        public override string ToString() => $"Theme ({Name})";

        public static readonly IEnumerable<ThemeSlot> CONSTANT_SLOTS = new[]
        {
            ThemeSlot.SevereWarning,
            ThemeSlot.SevereWarningBackground,
            ThemeSlot.Success,
            ThemeSlot.SuccessBackground,
            ThemeSlot.Warning,
            ThemeSlot.WarningBackground,
            ThemeSlot.Error,
            ThemeSlot.ErrorBackground,
            ThemeSlot.Transparent
        };
    }

    public static class ThemeExtensions
    {
        /// <summary>
        /// Applies the action to this theme.
        /// </summary>
        public static Theme With(this Theme theme, Action<Theme> action)
        {
            action?.Invoke(theme);
            return theme;
        }

        /// <summary>
        /// Merges this theme to the other but not overwriting existing slots.
        /// </summary>
        public static Theme Merge(this Theme theme, Theme other)
        {
            foreach (var slot in Enum.GetValues<ThemeSlot>())
                theme.Set(slot, other.Get(slot));

            return theme;
        }
    }
}
