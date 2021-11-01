// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Settings.Sections;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Settings
{
    public class SettingsOverlay : OverlayContainer
    {
        /// <summary>
        /// Gets whether the body is currently visible.
        /// </summary>
        public bool BodyVisible { get; private set; }

        /// <summary>
        /// Whether to keep the body hidden.
        /// </summary>
        public bool KeepBodyHidden { get; set; }

        /// <summary>
        /// Gets whether the navigation buttons are enabled.
        /// </summary>
        public bool NavigationButtonsEnabled
        {
            get => sidebar?.Children[0]?.Enabled.Value ?? false;
            private set
            {
                if (sidebar == null)
                    return;

                foreach (var button in sidebar.Children)
                    button.Enabled.Value = value;
            }
        }

        /// <summary>
        /// Whether the back button can be interacted with. See <see cref="Back"/> to programatically trigger this action even
        /// if the back button itself is disabled.
        /// </summary>
        public bool BackButtonEnabled
        {
            get => sidebar.BackButton.Enabled.Value;
            set => sidebar.BackButton.Enabled.Value = value;
        }

        /// <summary>
        /// Whether the exit button can be interacted with.
        /// </summary>
        public bool ExitButtonEnabled
        {
            get => sidebar.ExitButton.Enabled.Value;
            set => sidebar.ExitButton.Enabled.Value = value;
        }

        /// <summary>
        /// Gets what <see cref="SettingsSection"/> is currently selected.
        /// </summary>
        public SettingsSection CurrentSection => sidebar?.Children.FirstOrDefault(b => b.Active.Value)?.Section ?? sections.FirstOrDefault();

        private Container body;
        private SettingsHeader header;
        private SettingsSidebar sidebar;
        private SettingsMainPanel mainPanel;
        private Container<SettingsSubPanel> subPanelContainer;
        private readonly Stack<Action> backActionStack = new Stack<Action>();

        [Resolved(canBeNull: true)]
        private VignetteGame game { get; set; }

        public AvatarSection AvatarSection;

        private IEnumerable<SettingsSection> sections => mainPanel.Children.OfType<SettingsSection>();

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.X;
            RelativeSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                body = new Container
                {
                    Width = 350,
                    Margin = new MarginPadding { Left = SettingsSidebar.WIDTH },
                    RelativeSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new ThemableBox
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = ThemeSlot.Gray20,
                        },
                        new GridContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            RowDimensions = new[]
                            {
                                new Dimension(GridSizeMode.AutoSize),
                                new Dimension(),
                            },
                            Content = new Drawable[][]
                            {
                                new Drawable[]
                                {
                                    header = new SettingsHeader(),
                                },
                                new Drawable[]
                                {
                                    mainPanel = new SettingsMainPanel
                                    {
                                        Depth = 1,
                                        Children = new Drawable[]
                                        {
                                            new HomeSection(),
                                            new RecognitionSection(),
                                            AvatarSection = new AvatarSection(),
                                            new BackdropSection(),
                                            new SystemSection(),
                                            new FooterSection(),
                                        },
                                    },
                                },
                            }
                        },
                        subPanelContainer = new Container<SettingsSubPanel>
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
                sidebar = new SettingsSidebar
                {
                    BackButton = { Action = Back },
                    ExitButton = { Action = handleExitAction },
                    Children = sections.Select(s => new SettingsSidebarButton(s) { SelectionRequested = handleSectionSelection }).SkipLast(1).ToList(),
                },
            };

            header.SearchBox.Current.ValueChanged += e => mainPanel.SearchTerm = e.NewValue;
            setActiveButton(sections.FirstOrDefault());
        }

        /// <summary>
        /// Scrolls to a specific section. It forces <see cref="SettingsOverlay"/> to be shown if it isn't.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="SettingsSection"/> to scroll to.</typeparam>
        public void ScrollTo<T>()
            where T : SettingsSection
        {
            if (State.Value == Visibility.Hidden)
                Show();

            var item = sections.FirstOrDefault(s => s.GetType() == typeof(T));

            if (item == null)
                return;

            handleSectionSelection(item);
        }

        /// <summary>
        /// Shows the overlay's body. To show the overlay itself, see <see cref="VisibilityContainer.Show"/>.
        /// </summary>
        /// <remarks>May not take effect if <see cref="KeepBodyHidden"/> is true or <see cref="State"/> is <see cref="Visibility.Hidden"/>.</remarks>
        public void ShowBody() => showBody(true);

        /// <summary>
        /// Hides the overlay's body leaving the sidebar only visible. To hide the overlay itself, see <see cref="VisibilityContainer.Hide"/>.
        /// </summary>
        public void HideBody() => hideBody(true);

        /// <summary>
        /// Opens a sub panel and forcibly opens the overlay if it is hidden.
        /// </summary>
        public void ShowSubPanel(SettingsSubPanel panel)
        {
            if (panel == null)
                return;

            if (State.Value == Visibility.Hidden)
                Show();

            subPanelContainer.Add(panel);
            panel.Show();

            RegisterBackAction(() => closeSubPanel(panel));
        }

        /// <summary>
        /// Performs a back action. To hide the overlay itself, see <see cref="VisibilityContainer.Hide"/>.
        /// </summary>
        public void Back()
        {
            if (backActionStack.TryPop(out var action))
            {
                action.Invoke();
                return;
            }

            Hide();
        }

        /// <summary>
        /// Registers an action to be performed when the back button is pressed.
        /// </summary>
        public void RegisterBackAction(Action action) => backActionStack.Push(action);

        private void showBody(bool enableButtons = false)
        {
            if (State.Value == Visibility.Hidden || KeepBodyHidden)
                return;

            if (enableButtons)
                NavigationButtonsEnabled = true;

            body.MoveToX(0, 300, Easing.OutCirc);
            BodyVisible = true;
        }

        private void hideBody(bool disableButtons = false)
        {
            if (disableButtons)
                NavigationButtonsEnabled = false;

            body.MoveToX(-400, 300, Easing.InCirc);
            BodyVisible = false;
        }

        private void setActiveButton(SettingsSection section)
        {
            foreach (var button in sidebar.Children)
                button.Active.Value = button.Section == section;
        }

        private void closeSubPanel(SettingsSubPanel panel)
        {
            panel.Hide();
            panel.Expire();
        }

        private void handleSectionSelection(SettingsSection section)
        {
            foreach (var panel in subPanelContainer)
                closeSubPanel(panel);

            setActiveButton(section);
            mainPanel.ScrollContainer.ScrollTo(section);
        }

        private void handleExitAction()
        {
            game.Exit();
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            if (mainPanel.ScrollContainer.UserScrolling)
            {
                float currentScroll = mainPanel.ScrollContainer.Current;

                var section = mainPanel.Children.Where(c => c.IsPresent)
                                .OfType<SettingsSection>()
                                .TakeWhile(section => mainPanel.ScrollContainer.GetChildPosInContent(section) - currentScroll - 10 <= 0)
                                .LastOrDefault() ?? sections.FirstOrDefault();

                setActiveButton(section);
            }

            header.ShowLogo = CurrentSection == sections.FirstOrDefault();
        }

        protected override void PopIn()
        {
            FadeEdgeEffectTo(0.15f, 300);
            this.MoveToX(0, 300, Easing.OutCirc);
            showBody(true);
        }

        protected override void PopOut()
        {
            FadeEdgeEffectTo(0, 300);
            this.MoveToX(-SettingsSidebar.WIDTH, 300, Easing.InCirc);
            hideBody();
        }
    }
}
