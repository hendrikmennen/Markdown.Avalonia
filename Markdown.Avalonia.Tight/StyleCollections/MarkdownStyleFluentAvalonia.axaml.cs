using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleFluentAvalonia : Styles
    {
        public MarkdownStyleFluentAvalonia()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
