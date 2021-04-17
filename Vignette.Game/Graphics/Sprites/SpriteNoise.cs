// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Batches;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace Vignette.Game.Graphics.Sprites
{
    public class SpriteNoise : Drawable
    {
        private IShader shader;

        public Vector2 Resolution { get; set; } = Vector2.One;

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders)
        {
            shader = shaders.Load(VertexShaderDescriptor.TEXTURE_2, "SimplexNoise");
        }

        protected override DrawNode CreateDrawNode() => new SpriteNoiseDrawNode(this);

        private class SpriteNoiseDrawNode : DrawNode
        {
            protected new SpriteNoise Source => (SpriteNoise)base.Source;

            private readonly QuadBatch<TexturedVertex2D> batch = new QuadBatch<TexturedVertex2D>(1, 1);

            private readonly Action<TexturedVertex2D> addAction;

            private IShader shader;

            private Vector2 resolution;

            private Quad screenSpaceDrawQuad;

            public SpriteNoiseDrawNode(IDrawable source)
                : base(source)
            {
                addAction = v => batch.Add(new TexturedVertex2D
                {
                    Position = v.Position,
                    Colour = v.Colour,
                });
            }

            public override void ApplyState()
            {
                base.ApplyState();

                shader = Source.shader;
                resolution = Source.Resolution;
                screenSpaceDrawQuad = Source.ScreenSpaceDrawQuad;
            }

            public override void Draw(Action<TexturedVertex2D> vertexAction)
            {
                base.Draw(vertexAction);

                shader.Bind();

                shader.GetUniform<Vector2>("u_resolution").UpdateValue(ref resolution);

                DrawQuad(Texture.WhitePixel, screenSpaceDrawQuad, DrawColourInfo.Colour, vertexAction: addAction);

                shader.Unbind();
            }
        }
    }
}
