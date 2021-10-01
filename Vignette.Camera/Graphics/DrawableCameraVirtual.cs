// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

namespace Vignette.Camera.Graphics
{
    /// <summary>
    /// A <see cref="CameraVirtual"/> that can be added to the scene hierarchy.
    /// </summary>
    /// <remarks>The playback rate is unaffected by <see cref="Framework.Graphics.Drawable.Clock"/>.</remarks>
    public class DrawableCameraVirtual : DrawableCameraWrapper<CameraVirtual>, ICameraVirtual
    {
        /// <<inheritdoc cref="CameraVirtual.Loop"/>
        public bool Loop
        {
            get => Camera.Loop;
            set => Camera.Loop = value;
        }

        /// <<inheritdoc cref="CameraVirtual.FrameCount"/>
        public int FrameCount => Camera.FrameCount;

        public DrawableCameraVirtual(CameraVirtual camera, bool disposeUnderlyingCameraOnDispose = true)
            : base(camera, disposeUnderlyingCameraOnDispose)
        {
        }

        /// <<inheritdoc cref="CameraVirtual.Position"/>
        int ICameraVirtual.Position => Camera.Position;

        /// <<inheritdoc cref="CameraVirtual.Seek"/>
        public void Seek(int frame) => Camera.Seek(frame);
    }
}
