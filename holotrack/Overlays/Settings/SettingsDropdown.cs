using System.Collections.Generic;
using holotrack.Graphics.Interface;
using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace holotrack.Overlays.Settings
{
    public class SettingsDropdown<T> : SettingsItem<T>
    {
        private HoloTrackDropdown<T> control;

        protected override Drawable CreateControl() => control = new HoloTrackDropdown<T>
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