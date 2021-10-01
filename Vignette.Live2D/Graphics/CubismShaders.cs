// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System.Collections.Generic;
using osu.Framework.Graphics.Shaders;

namespace Vignette.Live2D.Graphics
{
    internal class CubismShaders
    {
        private readonly List<IShader> shaders = new List<IShader>();

        public IShader SetupMask => shaders[0];

        public CubismShaders(ShaderManager manager)
        {
            shaders.Add(manager.Load(VertexShaderDescriptor.SETUP_MASK, FragmentShaderDescriptor.SETUP_MASK));
            shaders.Add(manager.Load(VertexShaderDescriptor.SOURCE, FragmentShaderDescriptor.SOURCE));
            shaders.Add(manager.Load(VertexShaderDescriptor.SOURCE_MASK, FragmentShaderDescriptor.SOURCE_MASK));
            shaders.Add(manager.Load(VertexShaderDescriptor.SOURCE_MASK, FragmentShaderDescriptor.SOURCE_MASK_INVERT));
            shaders.Add(manager.Load(VertexShaderDescriptor.SOURCE, FragmentShaderDescriptor.SOURCE_PREMULT));
            shaders.Add(manager.Load(VertexShaderDescriptor.SOURCE_MASK, FragmentShaderDescriptor.SOURCE_MASK_PREMULT));
            shaders.Add(manager.Load(VertexShaderDescriptor.SOURCE_MASK, FragmentShaderDescriptor.SOURCE_MASK_INVERT_PREMULT));
        }

        public IShader GetShaderFor(bool hasMask, bool isInverted, bool usePremultipliedAlpha)
        {
            int index = 1 + (hasMask ? (isInverted ? 2 : 1) : 0) + (usePremultipliedAlpha ? 3 : 0);
            return shaders[index];
        }

        public static class VertexShaderDescriptor
        {
            public const string SETUP_MASK = "VertShaderSrcSetupMask";
            public const string SOURCE = "VertShaderSrc";
            public const string SOURCE_MASK = "VertShaderSrcMasked";
        }

        public static class FragmentShaderDescriptor
        {
            public const string SETUP_MASK = "FragShaderSrcSetupMask";
            public const string SOURCE = "FragShaderSrc";
            public const string SOURCE_MASK = "FragShaderSrcMask";
            public const string SOURCE_MASK_INVERT = "FragShaderSrcMaskInverted";
            public const string SOURCE_PREMULT = "FragShaderSrcPremultipliedAlpha";
            public const string SOURCE_MASK_PREMULT = "FragShaderSrcMaskPremultipliedAlpha";
            public const string SOURCE_MASK_INVERT_PREMULT = "FragShaderSrcMaskInvertedPremultipliedAlpha";
        }
    }
}
