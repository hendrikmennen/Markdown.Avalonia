using Avalonia.Media;

namespace ColorTextBlock.Avalonia.Geometries
{
    internal class LineBreakMarkGeometry : TextGeometry
    {
        internal LineBreakMarkGeometry(
            CInline owner,
            double lineHeight) :
            base(owner, 0, lineHeight, lineHeight, TextVerticalAlignment.Base, true)
        {
        }

        internal LineBreakMarkGeometry(CInline owner) :
            base(owner, 0, 0, 0, TextVerticalAlignment.Base, true)
        {
        }

        public override void Render(DrawingContext ctx)
        {
        }
    }
}