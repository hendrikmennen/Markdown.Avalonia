using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    internal class MarkdownStyleStandard : Styles, INamedStyle
    {
        public MarkdownStyleStandard()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string Name => nameof(MarkdownStyle.Standard);
        public bool IsEditted { get; set; }
    }
}