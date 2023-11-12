using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleFluentTheme : Styles, INamedStyle
    {
        public string Name => nameof(MarkdownStyle.FluentTheme);
        public bool IsEditted { get; set; }

        public MarkdownStyleFluentTheme()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
