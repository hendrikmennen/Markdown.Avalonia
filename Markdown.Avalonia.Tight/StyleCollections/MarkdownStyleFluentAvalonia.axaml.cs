using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    class MarkdownStyleFluentAvalonia : Styles, INamedStyle
    {
        public string Name => nameof(MarkdownStyle.FluentAvalonia);
        public bool IsEditted { get; set; }

        public MarkdownStyleFluentAvalonia()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
