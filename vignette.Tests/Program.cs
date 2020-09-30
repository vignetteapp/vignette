using osu.Framework;
using osu.Framework.Platform;

namespace vignette.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"vignette-tests"))
                host.Run(new VignetteTestBrowser());
        }
    }
}
