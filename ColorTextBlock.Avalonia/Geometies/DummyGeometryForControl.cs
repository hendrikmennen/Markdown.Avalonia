using Avalonia.Controls;
using Avalonia.Media;
using ColorTextBlock.Avalonia.Geometries;

namespace ColorTextBlock.Avalonia.Geometies
{
    internal class DummyGeometryForControl : CGeometry
    {
        public DummyGeometryForControl(Control control, TextVerticalAlignment alignment) :
            base(
                control.DesiredSize.Width,
                control.DesiredSize.Height,
                control.DesiredSize.Height,
                alignment,
                false)
        {
            Control = control;
        }

        public Control Control { get; }

        public override void Render(DrawingContext ctx)
        {
        }
    }
}