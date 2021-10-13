// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Extensions;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Graphics.Containers
{
    /// <summary>
    /// A container capable of displaying a single <see cref="View"/> at a time.
    /// </summary>
    public class ViewContainer : CompositeDrawable
    {
        /// <summary>
        /// The current view of this container.
        /// </summary>
        public IView CurrentView { get; private set; }

        /// <summary>
        /// Whether we show a spinner while the next view is loading.
        /// </summary>
        public virtual bool ShowSpinner => true;

        private readonly Container content;
        private readonly Spinner spinner;
        private IView lastView;

        public ViewContainer(IView first = null)
        {
            InternalChild = content = new Container { RelativeSizeAxes = Axes.Both };

            if (ShowSpinner)
            {
                AddInternal(spinner = new Spinner
                {
                    Size = new Vector2(48),
                    Alpha = 0,
                    Depth = 1,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                });
            }

            if (first != null)
                Show(first);
        }

        /// <summary>
        /// Makes the view current hiding the previous view.
        /// </summary>
        /// <param name="view">The view to present.</param>
        public void Show(IView view)
        {
            if (view == null)
                return;

            CurrentView?.OnExiting(lastView);
            CurrentView?.Hide();

            var viewDrawable = view.AsDrawable();

            if (!content.Contains(viewDrawable))
            {
                spinner?.Show();
                LoadComponentAsync(viewDrawable, loaded =>
                {
                    spinner?.Hide();
                    content.Add(viewDrawable);
                });
            }
            else
            {
                content.ChangeChildDepth(viewDrawable, CurrentView.AsDrawable()?.Depth + 1 ?? 0);
            }

            lastView = CurrentView;
            CurrentView = view;

            CurrentView?.OnEntering(lastView);
            CurrentView?.Show();
        }

        /// <summary>
        /// Moves the view back with the option of expiring the current view.
        /// </summary>
        /// <param name="expire">Should the current view be expired.</param>
        public void Return(bool expire = false)
        {
            var current = CurrentView?.AsDrawable();

            Show(lastView);

            if (expire)
                current?.Expire();
        }
    }
}
