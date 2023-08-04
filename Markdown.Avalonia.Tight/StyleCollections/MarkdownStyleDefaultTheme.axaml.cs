using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleDefaultTheme : Styles
    {
        public MarkdownStyleDefaultTheme()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
