using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    internal class MarkdownStyleFluentAvalonia : Styles, INamedStyle
    {
        public MarkdownStyleFluentAvalonia()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string Name => nameof(MarkdownStyle.FluentAvalonia);
        public bool IsEditted { get; set; }
    }
}