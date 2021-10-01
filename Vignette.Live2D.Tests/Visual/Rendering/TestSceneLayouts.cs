// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Bindables;
using osuTK;

namespace Vignette.Live2D.Tests.Visual.Rendering
{
    public class TestSceneLayouts : CubismModelTestScene
    {
        private readonly Bindable<Vector2> size = new Bindable<Vector2>();

        public TestSceneLayouts()
        {
            AddSliderStep<float>("width", 1, 1024, 1024, x => size.Value = new Vector2(x, size.Value.Y));
            AddSliderStep<float>("height", 1, 1024, 1024, y => size.Value = new Vector2(size.Value.X, y));
        }

        protected override void ModelChanged()
        {
            base.ModelChanged();

            size.UnbindEvents();
            size.BindValueChanged(s => Model.Size = s.NewValue, true);
        }
    }
}
