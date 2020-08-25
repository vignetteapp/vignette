using osu.Framework;
using osu.Framework.Platform;

namespace holotrack.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"holotrack-tests"))
                host.Run(new HoloTrackTestBrowser());
        }
    }
}
