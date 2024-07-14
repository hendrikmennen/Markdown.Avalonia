using System;
using Avalonia.Styling;
using Markdown.Avalonia.StyleCollections;

namespace Markdown.Avalonia
{
    public static class MarkdownStyle
    {
        public static Styles Standard => new MarkdownStyleStandard();

        [Obsolete("Use SimpleTheme instead")] public static Styles DefaultTheme => SimpleTheme;

        public static Styles SimpleTheme => new MarkdownStyleDefaultTheme();

        public static Styles FluentTheme => new MarkdownStyleFluentTheme();

        public static Styles FluentAvalonia => new MarkdownStyleFluentAvalonia();

        public static Styles GithubLike => new MarkdownStyleGithubLike();
    }
}