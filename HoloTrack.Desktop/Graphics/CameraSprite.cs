using osu.Framework.Graphics.Textures;
using System.IO;
using HoloTrack.Vision;

namespace HoloTrack.Desktop.Graphics
{
    class CameraSprite
    {
        /// <summary>
        /// Returns a Camera Stream in a osu! OpenGL Texture.
        /// </summary>
        public static CameraTexture CreateCameraTexture()
        {
            byte[] cameraStream = Camera.CreateCameraVideoByte();
            TextureUpload upload = new TextureUpload(new MemoryStream(cameraStream));
            Texture cameraTexture = new Texture(upload.Width, upload.Height);

            //set the texture then return it. We'll let the entire stuff do the rest.
            cameraTexture.SetData(upload);

            return new CameraTexture(cameraTexture);
        }
    }
}
