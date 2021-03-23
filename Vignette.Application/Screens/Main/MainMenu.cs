// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Backgrounds;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Input;

namespace Vignette.Application.Screens.Main
{
    [Cached]
    public class MainMenu : Screen, IKeyBindingHandler<GlobalAction>
    {
        public Toolbar Toolbar { get; private set; }

        private readonly BufferedContainer blurContainer;

        private readonly TextFlowContainer textFlow;

        private readonly Container helperInstructions;

        public readonly BindableBool AllowBackgroundAdjustments = new BindableBool();

        [Resolved]
        private ApplicationConfigManager config { get; set; }

        public MainMenu()
        {
            InternalChild = new BackgroundAdjustmentHelper
            {
                OnUserDrag = handleUserDrag,
                OnUserScroll = handleUserScroll,
                Children = new Drawable[]
                {
                    blurContainer = new BufferedContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = new Background
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                    },
                    helperInstructions = new Container
                    {
                        Alpha = 0,
                        Margin = new MarginPadding(20),
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        AutoSizeAxes = Axes.Both,
                        Masking = true,
                        CornerRadius = 5.0f,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Colour4.Black,
                                Alpha = 0.5f,
                            },
                            new FillFlowContainer
                            {
                                AutoSizeAxes = Axes.Both,
                                Spacing = new Vector2(0, 20),
                                Direction = FillDirection.Vertical,
                                Children = new Drawable[]
                                {
                                    textFlow = new TextFlowContainer
                                    {
                                        AutoSizeAxes = Axes.Both,
                                        TextAnchor = Anchor.TopLeft,
                                        Margin = new MarginPadding { Horizontal = 20, Top = 20 },
                                    },
                                    new ThemedTextButton
                                    {
                                        Width = 100,
                                        Text = "Apply",
                                        Action = commitBackgroundAdjustment,
                                        Margin = new MarginPadding { Horizontal = 20, Bottom = 20 },
                                    }
                                },
                            },
                        },
                    },
                },
            };

            textFlow.AddText("Left Mouse Button ", s => s.Font = SegoeUI.Black.With(size: 14));
            textFlow.AddText("to move the background.", s => s.Font = SegoeUI.Regular.With(size: 14));
            textFlow.NewParagraph();
            textFlow.AddText("Mouse Scroll ", s => s.Font = SegoeUI.Black.With(size: 14));
            textFlow.AddText("to resize the background.", s => s.Font = SegoeUI.Regular.With(size: 14));

            AllowBackgroundAdjustments.BindValueChanged(e => helperInstructions.FadeTo(e.NewValue ? 1 : 0, 500, Easing.OutQuint));
        }

        private void commitBackgroundAdjustment()
        {
            Toolbar.Show();
            AllowBackgroundAdjustments.Value = false;
        }

        private void handleUserDrag(Vector2 delta)
        {
            if (!AllowBackgroundAdjustments.Value)
                return;

            var posX = config.GetBindable<float>(ApplicationSetting.BackgroundOffsetX);
            var posY = config.GetBindable<float>(ApplicationSetting.BackgroundOffsetY);

            posX.Value += delta.X;
            posY.Value += delta.Y;
        }

        private void handleUserScroll(float delta)
        {
            if (!AllowBackgroundAdjustments.Value)
                return;

            var scale = config.GetBindable<float>(ApplicationSetting.BackgroundScaleXY);
            scale.Value += delta * 0.1f;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            LoadComponentAsync(Toolbar = new Toolbar(), AddInternal);
            Toolbar.State.ValueChanged += e => blurContainer.BlurTo(e.NewValue == Visibility.Visible ? new Vector2(10) : Vector2.Zero, 500, Easing.OutQuint);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleToolbar:
                    Toolbar?.ToggleVisibility();
                    return true;

                default:
                    return false;
            }
        }

        public void OnReleased(GlobalAction action)
        {
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeInFromZero(500, Easing.OutQuint);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Toolbar?.Hide();
            return base.OnClick(e);
        }

        private class BackgroundAdjustmentHelper : Container
        {
            public Action<Vector2> OnUserDrag;

            public Action<float> OnUserScroll;

            public BackgroundAdjustmentHelper()
            {
                RelativeSizeAxes = Axes.Both;
            }

            protected override bool OnDragStart(DragStartEvent e) => true;

            protected override void OnDrag(DragEvent e)
            {
                base.OnDrag(e);
                OnUserDrag?.Invoke(e.Delta);
            }

            protected override bool OnScroll(ScrollEvent e)
            {
                OnUserScroll?.Invoke(e.ScrollDelta.Y);
                return base.OnScroll(e);
            }
        }
    }
}
