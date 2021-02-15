// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Vignette.Application.Graphics.Sprites
{
    public class VignetteSpriteIcon : SpriteIcon, IThemeable
    {
        [Resolved(CanBeNull = true)]
        private VignetteColour colour { get; set; }

        private int level;

        public int Level
        {
            get => level;
            set
            {
                if (level == value)
                    return;

                level = value;
                updateColour();
            }
        }

        private Colouring colouring;

        public Colouring Colouring
        {
            get => colouring;
            set
            {
                if (colouring == value)
                    return;

                colouring = value;
                updateColour();
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            colour?.Accent.BindValueChanged(_ => updateColour());
            colour?.DarkMode.BindValueChanged(_ => updateColour());

            updateColour();
        }

        private void updateColour()
        {
            Schedule(() =>
            {
                switch (Colouring)
                {
                    case Colouring.Accent:
                        this.FadeColour(colour?.Accent.Value ?? Colour4.White, 200, Easing.OutQuint);
                        break;

                    case Colouring.Background:
                        this.FadeColour(colour?.GetBackgroundColor(Level) ?? Colour4.White, 200, Easing.OutQuint);
                        break;
                }
            });
        }
    }
}
