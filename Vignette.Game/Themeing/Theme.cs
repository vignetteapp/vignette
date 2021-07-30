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
        {
            Name = name;

            using var reader = new StreamReader(stream);
            addConstants();
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
                throw new InvalidOperationException("This theme already has a name and it cannot be overriden.");

            Name = name;
            return this;
        }

        /// <summary>
        /// Adds a colour to the colour mappings. If a slot is already defined, it will be overwritten.
        /// </summary>
        /// </summary>
        /// <param name="slot">The slot to add the colour to.</param>
        /// <param name="hex">The colour represented as a valid hex string.</param>
        /// <returns></returns>
        public Theme AddColour(ThemeSlot slot, string hex)
            => AddColour(slot, Colour4.FromHex(hex));

        /// <summary>
        /// Adds a colour to the colour mappings. If a slot is already defined, it will be overwritten.
        /// </summary>
        /// <param name="slot">The slot to add the colour to.</param>
        /// <param name="colour">The colour to set the slot's value to</param>
        /// <returns></returns>
        public Theme AddColour(ThemeSlot slot, Colour4 colour)
        {
            if (colours == null)
                colours = new Dictionary<ThemeSlot, Colour4>();

            if (colours.ContainsKey(slot))
                colours[slot] = colour;
            else
                colours.Add(slot, colour);

            return this;
        }

        public Colour4 GetColour(ThemeSlot slot)
        {
            if (colours.TryGetValue(slot, out Colour4 value))
                return value;

            throw new ArgumentException($"{slot} does not exist in '{this}' theme.");
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

            // neutralSecondaryAlt does not exist when created via the Theme Designer
            // we'll forcibly use a default instead
            AddColour(ThemeSlot.Gray110, "8a8886");
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

        private static Theme base_theme => new Theme()
            .AddColour(ThemeSlot.AccentDarker, "6b3172")
            .AddColour(ThemeSlot.AccentDark, "91439b")
            .AddColour(ThemeSlot.AccentDarkAlt, "ab4fb8")
            .AddColour(ThemeSlot.AccentPrimary, "BE58CB")
            .AddColour(ThemeSlot.AccentSecondary, "c669d2")
            .AddColour(ThemeSlot.AccentTertiary, "d794e0")
            .AddColour(ThemeSlot.AccentLight, "ebc7f0")
            .AddColour(ThemeSlot.AccentLighter, "f4e0f7")
            .AddColour(ThemeSlot.AccentLighterAlt, "fcf7fd");

        private static Theme base_theme_ins => new Theme()
            .AddColour(ThemeSlot.AccentDarker, "31724b")
            .AddColour(ThemeSlot.AccentDark, "439b66")
            .AddColour(ThemeSlot.AccentDarkAlt, "4fb879")
            .AddColour(ThemeSlot.AccentPrimary, "58CB86")
            .AddColour(ThemeSlot.AccentSecondary, "69d293")
            .AddColour(ThemeSlot.AccentTertiary, "94e0b2")
            .AddColour(ThemeSlot.AccentLight, "c7f0d7")
            .AddColour(ThemeSlot.AccentLighter, "e0f7e9")
            .AddColour(ThemeSlot.AccentLighterAlt, "f7fdf9");

        public static Theme GetLightTheme(bool useInsiderColor)
        {
            var theme = useInsiderColor ? base_theme_ins : base_theme;

            theme
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

            return theme;
        }

        public static Theme GetDarkTheme(bool useInsiderColor)
        {
            var theme = useInsiderColor ? base_theme_ins : base_theme;

            theme
                .SetName("Dark")
                .AddColour(ThemeSlot.Black, "ffffff")
                .AddColour(ThemeSlot.Gray190, "faf9f8")
                .AddColour(ThemeSlot.Gray160, "f3f2f1")
                .AddColour(ThemeSlot.Gray150, "edebe9")
                .AddColour(ThemeSlot.Gray130, "e1dfdd")
                .AddColour(ThemeSlot.Gray110, "d2d0ce")
                .AddColour(ThemeSlot.Gray90, "c8c6c4")
                .AddColour(ThemeSlot.Gray60, "a19f9d")
                .AddColour(ThemeSlot.Gray50, "8a8886")
                .AddColour(ThemeSlot.Gray40, "605e5c")
                .AddColour(ThemeSlot.Gray30, "3b3a39")
                .AddColour(ThemeSlot.Gray20, "323130")
                .AddColour(ThemeSlot.Gray10, "201f1e")
                .AddColour(ThemeSlot.White, "000000");

            return theme;
        }
    }
}
