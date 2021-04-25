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

        public static Theme Light = new Theme("Light")
            .AddColour(ThemeSlot.AccentDarker, "004578")
            .AddColour(ThemeSlot.AccentDark, "005a9e")
            .AddColour(ThemeSlot.AccentDarkAlt, "106ebe")
            .AddColour(ThemeSlot.AccentPrimary, "0078d4")
            .AddColour(ThemeSlot.AccentSecondary, "2b88d8")
            .AddColour(ThemeSlot.AccentTertiary, "71afe5")
            .AddColour(ThemeSlot.AccentLight, "c7e0f4")
            .AddColour(ThemeSlot.AccentLighter, "deecf9")
            .AddColour(ThemeSlot.AccentLighterAlt, "eff6fc")
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

        public static Theme Dark = new Theme("Dark")
            .AddColour(ThemeSlot.AccentDarker, "004578")
            .AddColour(ThemeSlot.AccentDark, "005a9e")
            .AddColour(ThemeSlot.AccentDarkAlt, "106ebe")
            .AddColour(ThemeSlot.AccentPrimary, "0078d4")
            .AddColour(ThemeSlot.AccentSecondary, "2b88d8")
            .AddColour(ThemeSlot.AccentTertiary, "71afe5")
            .AddColour(ThemeSlot.AccentLight, "c7e0f4")
            .AddColour(ThemeSlot.AccentLighter, "deecf9")
            .AddColour(ThemeSlot.AccentLighterAlt, "eff6fc")
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
    }
}
