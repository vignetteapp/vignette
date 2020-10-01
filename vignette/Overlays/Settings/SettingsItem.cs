using vignette.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace vignette.Overlays.Settings
{
    public abstract class SettingsItem<T> : FillFlowContainer
    {
        private VignetteSpriteText label;
        public virtual string Label
        {
            get => label?.Text ?? string.Empty;
            set
            {
                if (label == null)
                    Insert(-1, label = new VignetteSpriteText());

                label.Text = value;
            }
        }

        protected abstract Drawable CreateControl();
        protected Drawable Control { get; }
        private IHasCurrentValue<T> controlWithCurrent => Control as IHasCurrentValue<T>;
        public virtual Bindable<T> Bindable
        {
            get => controlWithCurrent.Current;
            set => controlWithCurrent.Current = value;
        }

        public SettingsItem()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Spacing = new Vector2(0, 5);
            Direction = FillDirection.Vertical;

            AddInternal(Control = CreateControl());
        }
    }
}