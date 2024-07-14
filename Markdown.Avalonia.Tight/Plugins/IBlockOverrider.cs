using System.Collections.Generic;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Markdown.Avalonia.Parsers;

namespace Markdown.Avalonia.Plugins
{
    public interface IBlockOverride
    {
        string ParserName { get; }

        IEnumerable<Control>? Convert(
            string text, Match firstMatch, ParseStatus status,
            IMarkdownEngine engine,
            out int parseTextBegin, out int parseTextEnd);
    }
}