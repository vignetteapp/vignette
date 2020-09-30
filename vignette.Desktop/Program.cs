using osu.Framework;
using osu.Framework.Platform;

namespace vignette.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"vignette"))
            using (Game game = new VignetteDesktop())
                host.Run(game);
        }
    }
}
