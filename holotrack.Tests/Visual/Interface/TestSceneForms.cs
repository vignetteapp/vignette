using holotrack.Graphics.Interface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneForms : TestScene
    {
        public TestSceneForms()
        {
            HoloTrackForm form;

            BufferedContainer container = new BufferedContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(100),
                    Rotation = 45,
                }
            };

            AddRange(new Drawable[]
            {
                container,
                form = new TestForm(container)
                {
                    Size = new Vector2(400, 300),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });

            AddToggleStep("toggle titlebar", v => form.TitleBarVisibility = v);
        }

        private class TestForm : HoloTrackForm
        {
            public TestForm(BufferedContainer buffer)
            {
                Title = "Draggable Test Form";
                Background.Child = buffer.CreateView().With(d =>
                {
                    d.RelativeSizeAxes = Axes.Both;
                    d.DisplayOriginalEffects = true;
                    d.SynchronisedDrawQuad = true;
                });
            }

            protected override bool OnDragStart(DragStartEvent e) => true;
            protected override void OnDrag(DragEvent e)
            {
                base.OnDrag(e);
                Position += e.Delta;
            }
        }
    }
}