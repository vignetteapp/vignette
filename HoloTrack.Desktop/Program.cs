using osu.Framework;
using osu.Framework.Platform;

namespace holotrack.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"holotrack"))
            using (Game game = new HoloTrackDesktop())
                host.Run(game);
        }
    }
}
