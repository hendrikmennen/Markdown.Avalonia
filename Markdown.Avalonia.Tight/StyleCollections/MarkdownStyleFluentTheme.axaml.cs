using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleFluentTheme : Styles
    {
        public MarkdownStyleFluentTheme()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
