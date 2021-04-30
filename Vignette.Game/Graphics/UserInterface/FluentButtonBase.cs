// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class FluentButtonBase : Button
    {
        private bool isHovered;

        private bool isPressed;

        protected ThemeSlot BackgroundResting { get; set; }

        protected ThemeSlot BackgroundHovered { get; set; }

        protected ThemeSlot BackgroundPressed { get; set; }

        protected ThemeSlot BackgroundDisabled { get; set; }

        protected ThemeSlot LabelResting { get; set; }

        protected ThemeSlot LabelDisabled { get; set; }

        public FluentButtonBase()
        {
            Enabled.BindValueChanged(_ => Scheduler.AddOnce(UpdateState), true);
        }

        protected void UpdateState()
        {
            if (Enabled.Value)
            {
                if (isPressed)
                    UpdateBackground(BackgroundPressed);
                else if (IsHovered)
                    UpdateBackground(BackgroundHovered);
                else
                    UpdateBackground(BackgroundResting);

                UpdateLabel(LabelResting);
            }
            else
            {
                UpdateBackground(BackgroundDisabled);
                UpdateLabel(LabelDisabled);
            }
        }

        protected virtual void UpdateBackground(ThemeSlot slot)
        {
        }

        protected virtual void UpdateLabel(ThemeSlot slot)
        {
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (isPressed)
                return false;

            isPressed = true;
            UpdateState();

            if (!Enabled.Value)
                return false;

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            if (!isPressed)
                return;

            isPressed = false;
            UpdateState();

            base.OnMouseUp(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (isHovered)
                return false;

            isHovered = true;
            UpdateState();

            if (!Enabled.Value)
                return false;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!isHovered)
                return;

            isHovered = false;
            UpdateState();

            base.OnHoverLost(e);
        }
    }
}
