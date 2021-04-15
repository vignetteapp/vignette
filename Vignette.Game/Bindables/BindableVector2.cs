// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osuTK;

namespace Vignette.Game.Bindables
{
    public class BindableVector2 : Bindable<Vector2>
    {
        public BindableVector2(Vector2 defaultValue = default)
            : base(defaultValue)
        {
        }

        public override string ToString() => $"{Value.X},{Value.Y}";

        public override void Parse(object input)
        {
            if (!(input is string str) || string.IsNullOrEmpty(str))
            {
                Value = default;
                return;
            }

            string[] values = str.Split(',');
            if (values.Length < 2)
            {
                Value = default;
                return;
            }

            var value = new Vector2();

            if (float.TryParse(values[0], out float x))
                value.X = x;

            if (float.TryParse(values[1], out float y))
                value.Y = y;

            Value = value;
        }
    }
}
