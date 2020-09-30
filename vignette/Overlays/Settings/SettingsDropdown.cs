using System.Collections.Generic;
using vignette.Graphics.Interface;
using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace vignette.Overlays.Settings
{
    public class SettingsDropdown<T> : SettingsItem<T>
    {
        private VignetteDropdown<T> control;

        protected override Drawable CreateControl() => control = new VignetteDropdown<T>
        {
            RelativeSizeAxes = Axes.X
        };

        public IEnumerable<T> Items
        {
            get => control.Items;
            set => control.Items = value;
        }

        public IBindableList<T> ItemSource
        {
            get => control.ItemSource;
            set => control.ItemSource = value;
        }
    }
}