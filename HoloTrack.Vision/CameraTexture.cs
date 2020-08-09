using osu.Framework.Graphics.Textures;

namespace HoloTrack.Vision
{
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
