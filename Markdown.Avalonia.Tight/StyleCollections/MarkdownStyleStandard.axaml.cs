using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleStandard : Styles
    {
        public MarkdownStyleStandard()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
