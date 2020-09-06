using System;
using holotrack.Graphics.Interface;
using osu.Framework.Graphics;

namespace holotrack.Overlays.Settings
{
    public class SettingsNumberBox<T> : SettingsItem<T>
        where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        protected override Drawable CreateControl() => new NumberBox<T>
        {
            RelativeSizeAxes = Axes.X,
        };
    }
}