// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class RecognitionSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.EyeShow;

        public override LocalisableString Label => "Recognition";

        public RecognitionSection()
        {
            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Label = "Camera",
                    Children = new Drawable[]
                    {
                        new CameraPreview(),
                        new SettingsDropdown<string>
                        {
                            Icon = SegoeFluent.Camera,
                            Label = "Device",
                            Items = new[] { "Default" },
                        },
                        new OpenSubPanelButton<AdvancedCameraSettingsPanel>
                        {
                            Label = "Adjust image settings",
                        },
                    }
                },
                new SettingsSubSection
                {
                    Label = "Tracking",
                    Children = new Drawable[]
                    {
                        new SettingsSwitch
                        {
                            Label = "Enable Tracking",
                        }
                    },
                },
            };
        }

        private class CameraPreview : Container
        {
            public CameraPreview()
            {
                Size = new Vector2(250, 140);
                Masking = true;
                CornerRadius = 5;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Black,
                    }
                };
            }
        }

        private class AdvancedCameraSettingsPanel : SettingsSubPanel
        {
        }
    }
}
