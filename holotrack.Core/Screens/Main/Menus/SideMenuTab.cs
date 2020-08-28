using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace holotrack.Core.Screens.Main.Menus
{
    public abstract class SideMenuTab : IEquatable<SideMenuTab>
    {
        public abstract string Name { get; }
        public abstract SideMenuTabContent CreateContent();
        public virtual IconUsage Icon => FontAwesome.Solid.QuestionCircle;

        public bool Equals(SideMenuTab other) => Name == other.Name;

        public abstract class SideMenuTabContent : FillFlowContainer
        {
            public SideMenuTabContent()
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
                Spacing = new Vector2(0, 5);
            }
        }
    }
}