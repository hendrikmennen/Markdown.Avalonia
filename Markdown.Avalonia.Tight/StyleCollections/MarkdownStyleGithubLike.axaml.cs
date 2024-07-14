using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Markdown.Avalonia.StyleCollections
{
    internal class MarkdownStyleGithubLike : Styles, INamedStyle
    {
        public MarkdownStyleGithubLike()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string Name => nameof(MarkdownStyle.GithubLike);
        public bool IsEditted { get; set; }
    }
}