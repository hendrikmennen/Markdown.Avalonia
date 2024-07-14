using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    internal class MarkdownStyleFluentTheme : Styles, INamedStyle
    {
        public MarkdownStyleFluentTheme()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string Name => nameof(MarkdownStyle.FluentTheme);
        public bool IsEditted { get; set; }
    }
}