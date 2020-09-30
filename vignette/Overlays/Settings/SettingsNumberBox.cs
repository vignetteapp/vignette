using System;
using vignette.Graphics.Interface;
using osu.Framework.Graphics;

namespace vignette.Overlays.Settings
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