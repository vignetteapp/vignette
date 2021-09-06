// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
        /// The currently open <see cref="SettingsPanel"/> other than the main panel.
        /// </summary>
        public SettingsPanel SubPanel { get; private set; }

        /// <summary>
        /// Gets whether the body is currently visible.
        /// </summary>
        public bool MainPanelActive { get; private set; }

        /// <summary>
        /// Gets whether the buttons are enabled.
        /// </summary>
        public bool Buttons
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
        /// Gets whether a sub panel is being shown.
        /// </summary>
        public bool SubPanelActive => SubPanel != null;

        /// <summary>
        /// Gets what <see cref="SettingsSection"/> is currently selected.
        /// </summary>
        public SettingsSection CurrentSection => sidebar?.Children.FirstOrDefault(b => b.Active.Value)?.Section ?? sections.FirstOrDefault();

        private Container body;
        private SettingsHeader header;
        private SettingsPanel content;
        private SettingsSidebar sidebar;
        private Container subMenuContainer;

        [Resolved(canBeNull: true)]
        private VignetteGame game { get; set; }

        private IEnumerable<SettingsSection> sections => content.Children.OfType<SettingsSection>();

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
                                new Dimension(GridSizeMode.Distributed),
                            },
                            Content = new Drawable[][]
                            {
                                new Drawable[]
                                {
                                    header = new SettingsHeader(),
                                },
                                new Drawable[]
                                {
                                    content = new SettingsPanel
                                    {
                                        Depth = 1,
                                        Children = new Drawable[]
                                        {
                                            new HomeSection(),
                                            new SystemSection(),
                                            new SceneSection(),
                                            new RecognitionSection(),
                                            new InputSection(),
                                            new FooterSection(),
                                        },
                                    },
                                },
                            }
                        },
                        subMenuContainer = new Container
                        {
                            X = -400,
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
                sidebar = new SettingsSidebar
                {
                    OnBack = handleBackAction,
                    OnExit = handleExitAction,
                    Children = sections.Select(s => new SettingsSidebarButton(s) { SelectionRequested = handleSectionSelection }).SkipLast(1).ToList(),
                },
            };

            header.SearchBox.Current.ValueChanged += e => content.SearchTerm = e.NewValue;
            setActiveButton(sections.FirstOrDefault());
        }

        /// <summary>
        /// Selects a section from the side panel. It forces <see cref="SettingsOverlay"/> to be shown if it isn't.
        /// </summary>
        /// <typeparam name="T">The <see cref="SettingsSection"/> as a type to select.</typeparam>
        public void SelectTab<T>()
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
        /// Shows the overlay's body.
        /// </summary>
        public void ShowBody() => showBody(true);

        /// <summary>
        /// Hides the overlay's body leaving the sidebar only visible.
        /// </summary>
        public void HideBody() => hideBody(true);

        /// <summary>
        /// Opens a sub panel in the side panel.
        /// </summary>
        /// <typeparam name="T">The type of submenu to open.</typeparam>
        public void OpenSubPanel(SettingsPanel panel)
        {
            if (panel == null)
                return;

            if (State.Value == Visibility.Hidden)
                Show();

            Buttons = false;

            subMenuContainer.Add(SubPanel = panel);
            subMenuContainer.MoveToX(0, 300, Easing.OutCirc);
        }

        /// <summary>
        /// Closes the currently open sub panel.
        /// </summary>
        public void CloseSubPanel()
        {
            if (SubPanel == null)
                return;

            if (MainPanelActive)
                Buttons = true;

            subMenuContainer.MoveToX(-400, 300, Easing.InCirc).Then().Schedule(() =>
            {
                subMenuContainer.Clear();
                SubPanel = null;
            });
        }

        private void showBody(bool enableButtons = false)
        {
            if (State.Value == Visibility.Hidden)
                return;

            if (enableButtons && SubPanel == null)
                Buttons = true;

            body.MoveToX(0, 300, Easing.OutCirc);
            MainPanelActive = true;
        }

        private void hideBody(bool disableButtons = false)
        {
            if (disableButtons)
                Buttons = false;

            body.MoveToX(-400, 300, Easing.InCirc);
            MainPanelActive = false;
        }

        private void setActiveButton(SettingsSection section)
        {
            foreach (var button in sidebar.Children)
                button.Active.Value = button.Section == section;
        }

        private void handleSectionSelection(SettingsSection section)
        {
            setActiveButton(section);
            content.ScrollContainer.ScrollTo(section);
        }

        private void handleBackAction()
        {
            if (SubPanel != null)
            {
                CloseSubPanel();
                return;
            }

            State.Value = Visibility.Hidden;
        }

        private void handleExitAction()
        {
            game.Exit();
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            if (content.ScrollContainer.UserScrolling)
            {
                float currentScroll = content.ScrollContainer.Current;

                var section = content.Children.Where(c => c.IsPresent)
                                .OfType<SettingsSection>()
                                .TakeWhile(section => content.ScrollContainer.GetChildPosInContent(section) - currentScroll - 10 <= 0)
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
            CloseSubPanel();
            FadeEdgeEffectTo(0, 300);
            this.MoveToX(-SettingsSidebar.WIDTH, 300, Easing.InCirc);
            hideBody();
        }
    }
}
