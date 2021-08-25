// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using Vignette.Game.Configuration.Converters;

namespace Vignette.Game.Themeing
{
    /// <summary>
    /// Contains a dictionary of slots and colours and has the ability to retrieve colours back for use.
    /// </summary>
    public class Theme
    {
        public string Name { get; private set; }

        private Dictionary<ThemeSlot, Colour4> colours;

        /// <summary>
        /// Create a theme with an existing stream.
        /// </summary>
        /// <param name="name">The name of this theme.</param>
        /// <param name="stream">The stream to deserialize.</param>
        public Theme(string name, Stream stream)
            : this(name)
        {
            using var reader = new StreamReader(stream);
            readFromString(reader.ReadToEnd());
        }

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
        /// Sets the name of an unnamed theme.
        /// </summary>
        /// <param name="name">The name of this theme.</param>
        public Theme SetName(string name)
        {
            if (!string.IsNullOrEmpty(Name))
                throw new InvalidOperationException("This theme already has a name.");

            Name = name;
            return this;
        }

        /// <summary>
        /// Adds a colour to the colour mappings. If a slot is already defined, it will be overwritten.
        /// </summary>
        /// </summary>
        /// <param name="slot">The slot to add the colour to.</param>
        /// <param name="hex">The colour represented as a valid hex string.</param>
        public Theme AddColour(ThemeSlot slot, string hex)
            => AddColour(slot, Colour4.FromHex(hex));

        /// <summary>
        /// Adds a colour to the colour mappings. This will not do anything if the slot has already a colour defined..
        /// </summary>
        /// <param name="slot">The slot to add the colour to.</param>
        /// <param name="colour">The colour to set the slot's value to</param>
        public Theme AddColour(ThemeSlot slot, Colour4 colour)
        {
            if (colours == null)
                colours = new Dictionary<ThemeSlot, Colour4>();

            if (!colours.ContainsKey(slot))
                colours.Add(slot, colour);

            return this;
        }

        /// <summary>
        /// Gets the colour of the specified <see cref="ThemeSlot"/>.
        /// </summary>
        /// <param name="slot">The colour slot to retrieve.</param>
        public Colour4 GetColour(ThemeSlot slot)
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

        private void readFromString(string str)
        {
            var rawDict = JsonConvert.DeserializeObject<Dictionary<string, Colour4>>(str, new HexStringToColour4Converter());
            foreach ((string key, Colour4 value) in rawDict)
            {
                foreach (var slot in Enum.GetValues<ThemeSlot>())
                {
                    if (colours.ContainsKey(slot))
                        continue;

                    if (slot.GetDescription() == key)
                    {
                        AddColour(slot, value);
                        break;
                    }
                }
            }
        }

        private void addConstants()
        {
            AddColour(ThemeSlot.Transparent, Colour4.Transparent);
            AddColour(ThemeSlot.Error, "A80000");
            AddColour(ThemeSlot.ErrorBackground, "FDE7E9");
            AddColour(ThemeSlot.Success, "107C10");
            AddColour(ThemeSlot.SuccessBackground, "DFF6DD");
            AddColour(ThemeSlot.SevereWarning, "D83B01");
            AddColour(ThemeSlot.SevereWarningBackground, "FED9CC");
            AddColour(ThemeSlot.Warning, "797775");
            AddColour(ThemeSlot.WarningBackground, "FFF4CE");
        }

        public override string ToString() => Name;

        private static Theme getBaseTheme()
        {
            if (VignetteGameBase.IsInsidersBuild)
            {
                return new Theme()
                    .AddColour(ThemeSlot.AccentDarker, "31724b")
                    .AddColour(ThemeSlot.AccentDark, "439b66")
                    .AddColour(ThemeSlot.AccentDarkAlt, "4fb879")
                    .AddColour(ThemeSlot.AccentPrimary, "58CB86")
                    .AddColour(ThemeSlot.AccentSecondary, "69d293")
                    .AddColour(ThemeSlot.AccentTertiary, "94e0b2")
                    .AddColour(ThemeSlot.AccentLight, "c7f0d7")
                    .AddColour(ThemeSlot.AccentLighter, "e0f7e9")
                    .AddColour(ThemeSlot.AccentLighterAlt, "f7fdf9");
            }
            else
            {
                return new Theme()
                    .AddColour(ThemeSlot.AccentDarker, "6b3172")
                    .AddColour(ThemeSlot.AccentDark, "91439b")
                    .AddColour(ThemeSlot.AccentDarkAlt, "ab4fb8")
                    .AddColour(ThemeSlot.AccentPrimary, "BE58CB")
                    .AddColour(ThemeSlot.AccentSecondary, "c669d2")
                    .AddColour(ThemeSlot.AccentTertiary, "d794e0")
                    .AddColour(ThemeSlot.AccentLight, "ebc7f0")
                    .AddColour(ThemeSlot.AccentLighter, "f4e0f7")
                    .AddColour(ThemeSlot.AccentLighterAlt, "fcf7fd");
            }
        }

        public static readonly Theme Light = getBaseTheme()
            .SetName("Light")
            .AddColour(ThemeSlot.Black, "000000")
            .AddColour(ThemeSlot.Gray190, "201f1e")
            .AddColour(ThemeSlot.Gray160, "323130")
            .AddColour(ThemeSlot.Gray150, "3b3a39")
            .AddColour(ThemeSlot.Gray130, "605e5c")
            .AddColour(ThemeSlot.Gray110, "8a8886")
            .AddColour(ThemeSlot.Gray90, "a19f9d")
            .AddColour(ThemeSlot.Gray60, "c8c6c4")
            .AddColour(ThemeSlot.Gray50, "d2d0ce")
            .AddColour(ThemeSlot.Gray40, "e1dfdd")
            .AddColour(ThemeSlot.Gray30, "edebe9")
            .AddColour(ThemeSlot.Gray20, "f3f2f1")
            .AddColour(ThemeSlot.Gray10, "faf9f8")
            .AddColour(ThemeSlot.White, "ffffff");

        public static readonly Theme Dark = getBaseTheme()
            .SetName("Dark")
            .AddColour(ThemeSlot.Black, "f8f8f8")
            .AddColour(ThemeSlot.Gray190, "f4f4f4")
            .AddColour(ThemeSlot.Gray160, "ffffff")
            .AddColour(ThemeSlot.Gray150, "dadada")
            .AddColour(ThemeSlot.Gray130, "d0d0d0")
            .AddColour(ThemeSlot.Gray110, "8a8886")
            .AddColour(ThemeSlot.Gray90, "c8c8c8")
            .AddColour(ThemeSlot.Gray60, "6d6d6d")
            .AddColour(ThemeSlot.Gray50, "4f4f4f")
            .AddColour(ThemeSlot.Gray40, "484848")
            .AddColour(ThemeSlot.Gray30, "3f3f3f")
            .AddColour(ThemeSlot.Gray20, "313131")
            .AddColour(ThemeSlot.Gray10, "282828")
            .AddColour(ThemeSlot.White, "1f1f1f");
    }
}
