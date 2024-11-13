using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    internal class MarkdownStyleDefaultTheme : Styles, INamedStyle
    {
        public MarkdownStyleDefaultTheme()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string Name => nameof(MarkdownStyle.SimpleTheme);
        public bool IsEditted { get; set; }
    }
}