using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class BasicModal : Container
    {
        public BasicModal()
        {
            Masking = true;
            CornerRadius = 5;

            Add(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.FromHex("362f2d")
            });
        }
    }
}