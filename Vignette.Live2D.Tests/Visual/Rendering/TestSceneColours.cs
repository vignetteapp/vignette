// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Live2D.Tests.Visual.Rendering
{
    public class TestSceneColours : CubismModelTestScene
    {
        private Bindable<Colour4> colour = new Bindable<Colour4>(Colour4.White);
        private BindableFloat alpha = new BindableFloat
        {
            MinValue = 0.0f,
            MaxValue = 1.0f,
            Default = 1.0f,
            Value = 1.0f,
        };

        public TestSceneColours()
        {
            Extras.AddRange(new Drawable[]
            {
                new BasicSliderBar<float>
                {
                    Width = 300,
                    Height = 40,
                    Current = alpha,
                },
                new BasicColourPicker
                {
                    Margin = new MarginPadding { Top = 40 },
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Current = colour,
                }
            });
        }

        protected override void ModelChanged()
        {
            base.ModelChanged();

            colour.UnbindEvents();
            colour.BindValueChanged(c => Model.Colour = c.NewValue, true);

            alpha.UnbindEvents();
            alpha.BindValueChanged(a => Model.Alpha = a.NewValue, true);
        }
    }
}
