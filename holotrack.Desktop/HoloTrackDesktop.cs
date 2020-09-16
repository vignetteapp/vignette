using System.Reflection;
using osu.Framework.Platform;

namespace holotrack.Desktop
{
    public class HoloTrackDesktop : HoloTrackGame
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            switch (host.Window)
            {
                // Legacy osuTK Window
                case DesktopGameWindow desktopGameWindow:
                    desktopGameWindow.SetIconFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "logo.ico"));
                    desktopGameWindow.Title = Name;
                    desktopGameWindow.FileDrop += (_, e) => fileDrop(e.FileNames);
                    break;

                // SDL2 Window
                case DesktopWindow desktopWindow:
                    desktopWindow.Title = Name;
                    desktopWindow.DragDrop += f => fileDrop(new[] { f });
                    break;
            }
        }

        private void fileDrop(string[] paths)
        {
            foreach (var path in paths)
                Import(path);
        }
    }
}