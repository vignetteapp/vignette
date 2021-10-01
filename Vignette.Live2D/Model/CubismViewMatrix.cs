// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Graphics.Primitives;
using osuTK;

namespace Vignette.Live2D.Model
{
    public class CubismViewMatrix
    {
        public RectangleF ScreenRectangle { get; private set; }

        public RectangleF MaxRectangle { get; private set; }

        public float MaxScale { get; private set; }

        public float MinScale { get; private set; }

        private Matrix4 matrix = Matrix4.Identity;

        public CubismViewMatrix(RectangleF screen, RectangleF maxRect, float min, float max)
        {
            ScreenRectangle = screen;
            MaxRectangle = maxRect;
            MaxScale = max;
            MinScale = min;
        }

        public void Translate(float x, float y)
        {
            if (matrix[0, 0] * MaxRectangle.Left + (matrix[3, 0] + x) > ScreenRectangle.Left)
            {
                x = ScreenRectangle.Left - matrix[0, 0] * MaxRectangle.Left - matrix[3, 0];
            }

            if (matrix[0, 0] * MaxRectangle.Right + (matrix[3, 0] + x) < ScreenRectangle.Right)
            {
                x = ScreenRectangle.Right - matrix[0, 0] * MaxRectangle.Right - matrix[3, 0];
            }

            if (matrix[1, 1] * MaxRectangle.Top + (matrix[3, 1] + y) < ScreenRectangle.Top)
            {
                y = ScreenRectangle.Top - matrix[1, 1] * MaxRectangle.Top - matrix[3, 1];
            }

            if (matrix[1, 1] * MaxRectangle.Bottom + (matrix[3, 1] + y) < ScreenRectangle.Bottom)
            {
                y = ScreenRectangle.Bottom - matrix[1, 1] * MaxRectangle.Bottom - matrix[3, 1];
            }

            var mat = Matrix4.Identity;
            mat[3, 0] = x;
            mat[3, 1] = y;

            matrix *= mat;
        }

        public void Translate(Vector2 position)
        {
            Translate(position.X, position.Y);
        }

        public void Scale(float cx, float cy, float scale)
        {
            float targetScale = scale * matrix[0, 0];

            if (targetScale < MinScale)
            {
                if (matrix[0, 0] > 0.0f)
                {
                    scale = MinScale / matrix[0, 0];
                }
            }
            else if (targetScale > MaxScale)
            {
                if (matrix[0, 0] > 0.0f)
                {
                    scale = MaxScale / matrix[0, 0];
                }
            }

            var mat1 = Matrix4.Identity;
            mat1[3, 0] = cx;
            mat1[3, 1] = cy;

            var mat2 = Matrix4.Identity;
            mat2[0, 0] = scale;
            mat2[1, 1] = scale;

            var mat3 = Matrix4.Identity;
            mat3[3, 0] = -cx;
            mat3[3, 1] = -cy;

            matrix *= mat3;
            matrix *= mat2;
            matrix *= mat1;
        }

        public void Scale(Vector2 axes, float scale)
        {
            Scale(axes.X, axes.Y, scale);
        }
    }
}
