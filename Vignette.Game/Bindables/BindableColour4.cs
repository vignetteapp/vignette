// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace Vignette.Game.Bindables
{
    public class BindableColour4 : Bindable<Colour4>
    {
        public BindableColour4(Colour4 defaultValue = default)
            : base(defaultValue)
        {
        }

        public override string ToString() => Value.ToHex();

        public override void Parse(object input)
        {
            if (!(input is string str) || string.IsNullOrEmpty(str))
            {
                Value = default;
                return;
            }

            Value = Colour4.FromHex(str);
        }

        protected override Bindable<Colour4> CreateInstance() => new BindableColour4();
    }
}
