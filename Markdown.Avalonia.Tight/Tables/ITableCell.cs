using Avalonia.Layout;
using Avalonia.Media;

namespace Markdown.Avalonia.Tables
{
    internal interface ITableCell
    {
        int ColumnIndex { get; }

        string? RawText { get; }
        string? Text { get; }
        int RowSpan { get; }
        int ColSpan { get; }
        TextAlignment? Horizontal { get; }
        VerticalAlignment? Vertical { get; }
    }
}