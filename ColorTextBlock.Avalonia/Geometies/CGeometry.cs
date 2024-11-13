using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace ColorTextBlock.Avalonia.Geometries
{
    public abstract class CGeometry
    {
        public CGeometry(
            double width, double height, double baseHeight,
            TextVerticalAlignment textVerticalAlignment,
            bool linebreak)
        {
            Width = width;
            Height = height;
            BaseHeight = baseHeight;
            TextVerticalAlignment = textVerticalAlignment;
            LineBreak = linebreak;
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; }
        public double Height { get; }
        public double BaseHeight { get; }
        public bool LineBreak { get; }
        public TextVerticalAlignment TextVerticalAlignment { get; }

        public virtual Action<Control>? OnMouseEnter { get; set; }
        public virtual Action<Control>? OnMouseLeave { get; set; }
        public virtual Action<Control>? OnMousePressed { get; set; }
        public virtual Action<Control>? OnMouseReleased { get; set; }
        public virtual Action<Control>? OnClick { get; set; }

        public event Action? RepaintRequested;

        public abstract void Render(DrawingContext ctx);

        internal void RequestRepaint()
        {
            RepaintRequested?.Invoke();
        }
    }
}