// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Primitives;
using osuTK;
using osuTK.Graphics.ES30;

namespace Vignette.Live2D.Graphics
{
    internal unsafe partial class CubismRenderer
    {
        protected class CubismRendererDrawNode : TexturedShaderDrawNode
        {
            protected new CubismRenderer Source => (CubismRenderer)base.Source;

            private CubismShaders shaders;
            private List<CubismDrawable> drawables;
            private List<MaskingContext> contexts;

            public CubismRendererDrawNode(CubismRenderer source)
                : base(source)
            {
            }

            public override void ApplyState()
            {
                base.ApplyState();
                shaders = Source.shaders;
                contexts = Source.model.MaskingContexts.ToList();
                drawables = Source.model.Drawables.OrderBy(d => d.RenderOrder).ToList();
            }

            public override void Draw(Action<TexturedVertex2D> vertexAction)
            {
                base.Draw(vertexAction);

                // Draw our masks to the frame buffers
                // TODO: Use one frame buffer for each model
                foreach (var context in contexts)
                {
                    using (bindFrameBuffer(context.FrameBuffer))
                    {
                        var shader = shaders.SetupMask;
                        shader.Bind();

                        shader.GetUniform<int>("s_texture0").Value = 0;
                        shader.GetUniform<Vector4>("u_channelFlag").Value = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
                        shader.GetUniform<Matrix4>("u_clipMatrix").Value = context.MaskMatrix;
                        shader.GetUniform<Vector4>("u_baseColor").Value = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f);

                        foreach (var mask in context.Masks)
                        {
                            if (mask.IsDoubleSided)
                            {
                                GL.Enable(EnableCap.CullFace);
                            }
                            else
                            {
                                GL.Disable(EnableCap.CullFace);
                            }

                            mask.Draw();
                        }

                        shader.Unbind();
                    }
                }

                // Draw our drawables
                foreach (var drawable in drawables)
                {
                    bool hasMask = drawable.MaskingContext != null;

                    var shader = shaders.GetShaderFor(hasMask, drawable.IsInvertedMask, false);

                    shader.Bind();

                    if (hasMask)
                    {
                        drawable.MaskingContext.FrameBuffer.Texture?.Bind(TextureUnit.Texture1);
                        shader.GetUniform<int>("s_texture1").Value = 1;
                        shader.GetUniform<Matrix4>("u_clipMatrix").Value = drawable.MaskingContext.DrawMatrix;
                        shader.GetUniform<Vector4>("u_channelFlag").Value = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        GLWrapper.BindTexture(null, TextureUnit.Texture1);
                    }

                    drawable.Texture?.TextureGL.Bind();
                    GLWrapper.SetBlend(drawable.Blending);

                    shader.GetUniform<int>("s_texture0").Value = 0;
                    shader.GetUniform<Matrix4>("u_matrix").Value = new Matrix4(DrawInfo.Matrix);
                    shader.GetUniform<Vector4>("u_baseColor").Value = new Vector4(1, 1, 1, drawable.Alpha);

                    if (drawable.IsDoubleSided)
                    {
                        GL.Enable(EnableCap.CullFace);
                    }
                    else
                    {
                        GL.Disable(EnableCap.CullFace);
                    }

                    drawable.Draw();

                    GLWrapper.SetBlend(BlendingParameters.None);
                    shader.Unbind();
                }

                // Ensure that culling is disabled after drawing
                GL.Disable(EnableCap.CullFace);
            }

            private IDisposable bindFrameBuffer(FrameBuffer frameBuffer)
            {
                frameBuffer.Bind();
                GLWrapper.PushViewport(new RectangleI(0, 0, frameBuffer.Texture.Width, frameBuffer.Texture.Height));
                GLWrapper.Clear(new ClearInfo(Colour4.White));
                GLWrapper.SetBlend(new BlendingParameters
                {
                    Source = BlendingType.Zero,
                    Destination = BlendingType.OneMinusSrcColor,
                    SourceAlpha = BlendingType.Zero,
                    DestinationAlpha = BlendingType.OneMinusSrcAlpha,
                });

                return new ValueInvokeOnDisposal<FrameBuffer>(frameBuffer, f => unbindFrameBuffer(f));
            }

            private void unbindFrameBuffer(FrameBuffer frameBuffer)
            {
                GLWrapper.SetBlend(BlendingParameters.None);
                GLWrapper.PopViewport();
                frameBuffer.Unbind();
            }
        }
    }
}
