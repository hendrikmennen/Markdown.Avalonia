using Avalonia;
using Avalonia.Media;

namespace ColorTextBlock.Avalonia.Geometries
{
    public class ImageGeometry : CGeometry
    {
        internal ImageGeometry(IImage image, double width, double height,
            TextVerticalAlignment alignment) : base(width, height, height, alignment, false)
        {
            Image = image;
            Width = width;
            Height = height;
        }

        public new double Width { get; }
        public new double Height { get; }
        public IImage Image { get; }

        public override void Render(DrawingContext ctx)
        {
            ctx.DrawImage(
                Image,
                new Rect(Image.Size),
                new Rect(Left, Top, Width, Height));
        }
    }
}