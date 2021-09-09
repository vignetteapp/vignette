// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Containers
{
    public class FluentMarkdownContainer : MarkdownContainer
    {
        public override MarkdownTextFlowContainer CreateTextFlow()
            => new ThemableMarkdownFlowContainer();

        protected override MarkdownList CreateList(ListBlock listBlock)
            => new ThemableMarkdownList();

        protected class ThemableMarkdownFlowContainer : MarkdownTextFlowContainer
        {
            protected override SpriteText CreateSpriteText()
            {
                var themable = new ThemableSpriteText(false) { Colour = ThemeSlot.Gray190 };
                Schedule(() => AddInternal(themable));

                return themable.Create().With(d => d.Font = SegoeUI.Regular.With(size: 16));
            }

            protected override void AddLinkText(string text, LinkInline linkInline)
                => AddDrawable(new ThemableMarkdownLinkText(text, linkInline));
        }

        protected class ThemableMarkdownList : MarkdownList
        {
            public ThemableMarkdownList()
            {
                Padding = new MarginPadding();
            }
        }

        protected class ThemableMarkdownLinkText : CompositeDrawable
        {
            public string TooltipText => link;

            private string text;

            private string link;

            public ThemableMarkdownLinkText(string text, LinkInline linkInline)
            {
                this.text = text;
                this.link = linkInline.Url ?? string.Empty;
                AutoSizeAxes = Axes.Both;
            }

            [BackgroundDependencyLoader]
            private void load(GameHost host)
            {
                InternalChildren = new Drawable[]
                {
                    new ClickableContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Action = () => host.OpenUrlExternally(link),
                        Child = new ThemableSpriteText
                        {
                            Text = text,
                            Font = SegoeUI.Regular.With(size: 16),
                            Colour = ThemeSlot.AccentPrimary,
                        }
                    }
                };
            }
        }
    }
}
