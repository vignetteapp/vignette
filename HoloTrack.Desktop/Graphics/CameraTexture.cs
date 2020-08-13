using osu.Framework.Graphics.Textures;

namespace HoloTrack.Desktop.Graphics
{
    /// <summary>
    /// A class that represents a osu! GL texture based on the OpenCV data.
    /// </summary>
    public class CameraTexture
    {
        private readonly Texture texture;

        public CameraTexture(int width, int height)
        {
            texture = new Texture(width, height);
        }

        public CameraTexture(Texture texture)
        {
            this.texture = texture;
        }

    }
}
