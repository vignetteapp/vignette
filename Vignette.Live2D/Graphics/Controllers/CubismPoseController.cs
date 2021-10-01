// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using Vignette.Live2D.IO.Serialization;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that manages a <see cref="CubismModel"/>'s pose.
    /// </summary>
    public class CubismPoseController : CubismController
    {
        public float FadeInTime { get; private set; }

        private IEnumerable<IEnumerable<CubismPartInfo>> groups;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (string.IsNullOrEmpty(Model.Settings.FileReferences.Pose))
                return;

            using var stream = Model.Resources.GetStream(Model.Settings.FileReferences.Pose);
            var setting = CubismUtils.ReadJsonSetting<CubismPoseSetting>(stream);

            FadeInTime = setting.FadeInTime;

            groups = setting.Groups.Select(settingGroup =>
                settingGroup.Select(settingGroupItem =>
                    new CubismPartInfo
                    {
                        Parent = Model.Parts.FirstOrDefault(p => p.Name == settingGroupItem.Id),
                        Children = settingGroupItem.Link.Select(l => Model.Parts.FirstOrDefault(p => p.Name == l))
                    }
                )
            );

            Reset();
        }

        public void Reset()
        {
            foreach (var group in groups)
            {
                bool first = true;
                foreach (var item in group)
                {
                    float value = first ? 1.0f : 0.0f;
                    item.Parent.CurrentOpacity = value;
                    item.Parent.TargetOpacity = value;
                    first = false;
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            foreach (var group in groups)
                doFade((float)(Clock.ElapsedFrameTime / 1000), group);

            copyPartOpacities();
        }

        private void copyPartOpacities()
        {
            foreach (var group in groups)
            {
                foreach (var item in group)
                {
                    if (!item.Children.Any())
                        continue;

                    float opacity = item.Parent.CurrentOpacity;
                    foreach (var child in item.Children)
                        child.CurrentOpacity = opacity;
                }
            }
        }

        private void doFade(float delta, IEnumerable<CubismPartInfo> group)
        {
            if (!group.Any())
                return;

            const float epsilon = 0.001f;
            const float phi = 0.5f;
            const float back_opacity_threshold = 0.15f;

            float newOpacity = 1.0f;

            var visible = group.First();
            foreach (var item in group)
            {
                if (epsilon < item.Parent.TargetOpacity)
                {
                    visible = item;

                    newOpacity = item.Parent.CurrentOpacity;
                    newOpacity += delta / FadeInTime;
                    newOpacity = MathF.Min(newOpacity, 1.0f);
                    break;
                }
            }

            foreach (var item in group)
            {
                if (item == visible)
                    item.Parent.CurrentOpacity = newOpacity;
                else
                {
                    float opacity = item.Parent.CurrentOpacity;
                    float a1 = newOpacity < phi
                        ? newOpacity * (phi - 1.0f) / phi + 1.0f
                        : (1.0f - newOpacity) * phi / (1.0f - phi);

                    float backOpacity = (1.0f - a1) * (1.0f - newOpacity);
                    if (back_opacity_threshold < backOpacity)
                        a1 = 1.0f - back_opacity_threshold / (1.0f - newOpacity);

                    opacity = MathF.Min(opacity, a1);
                    item.Parent.CurrentOpacity = opacity;
                }
            }
        }

        private class CubismPartInfo
        {
            private CubismPart parent;

            public CubismPart Parent
            {
                get => parent;
                init
                {
                    parent = value;
                    parent.TargetOpacity = 1.0f;
                }
            }

            public IEnumerable<CubismPart> Children { get; set; }
        }
    }
}
