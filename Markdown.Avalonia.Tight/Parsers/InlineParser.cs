using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ColorTextBlock.Avalonia;

namespace Markdown.Avalonia.Parsers
{
    public abstract class InlineParser
    {
        public InlineParser(Regex pattern, string name)
        {
            Pattern = pattern;
            Name = name;
        }

        public Regex Pattern { get; }

        public string Name { get; }

        public abstract IEnumerable<CInline> Convert(
            string text, Match firstMatch,
            IMarkdownEngine engine,
            out int parseTextBegin, out int parseTextEnd);

        public static InlineParser New(Regex pattern, string name, Func<Match, CInline> v1)
        {
            return new Single(pattern, name, v1);
        }

        public static InlineParser New(Regex pattern, string name, Func<Match, IEnumerable<CInline>> v2)
        {
            return new Multi(pattern, name, v2);
        }

        private abstract class Wrapper : InlineParser
        {
            public Wrapper(Regex pattern, string name) : base(pattern, name)
            {
            }

            public override IEnumerable<CInline> Convert(
                string text, Match firstMatch,
                IMarkdownEngine engine,
                out int parseTextBegin, out int parseTextEnd)
            {
                parseTextBegin = firstMatch.Index;
                parseTextEnd = parseTextBegin + firstMatch.Length;
                return Convert(firstMatch);
            }

            public abstract IEnumerable<CInline> Convert(Match match);
        }

        private sealed class Single : Wrapper
        {
            private readonly Func<Match, CInline> converter;

            public Single(Regex pattern, string name, Func<Match, CInline> converter) : base(pattern, name)
            {
                this.converter = converter;
            }

            public override IEnumerable<CInline> Convert(Match match)
            {
                yield return converter(match);
            }
        }

        private sealed class Multi : Wrapper
        {
            private readonly Func<Match, IEnumerable<CInline>> converter;

            public Multi(Regex pattern, string name, Func<Match, IEnumerable<CInline>> converter) : base(pattern, name)
            {
                this.converter = converter;
            }

            public override IEnumerable<CInline> Convert(Match match)
            {
                return converter(match);
            }
        }
    }
}