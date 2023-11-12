using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleDefaultTheme : Styles, INamedStyle
    {
        public string Name => nameof(MarkdownStyle.SimpleTheme);
        public bool IsEditted { get; set; }

        public MarkdownStyleDefaultTheme()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
