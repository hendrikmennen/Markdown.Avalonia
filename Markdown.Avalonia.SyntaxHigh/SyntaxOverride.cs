using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using Markdown.Avalonia.Parsers;
using Markdown.Avalonia.Plugins;
using TextMateSharp.Themes;

namespace Markdown.Avalonia.SyntaxHigh
{
    public class SyntaxOverride : IBlockOverride
    {
        public static IRawTheme? CurrentEditorTheme;
        public static IAdvancedRegistryOptions? RegistryOptions;
        private readonly SetupInfo _info;
        private SyntaxHighlightProvider _provider;


        public SyntaxOverride(ObservableCollection<Alias> aliases, SetupInfo info)
        {
            _provider = new SyntaxHighlightProvider(aliases);
            _info = info;
        }

        public string ParserName => "CodeBlocksWithLangEvaluator";

        public IEnumerable<Control>? Convert(
            string text,
            Match match,
            ParseStatus status,
            IMarkdownEngine engine,
            out int parseTextBegin, out int parseTextEnd)
        {
            var closeTagPattern = new Regex($"\n[ ]*{match.Groups[1].Value}[ ]*\n");
            var closeTagMatch = closeTagPattern.Match(text, match.Index + match.Length);

            int codeEndIndex;
            if (closeTagMatch.Success)
            {
                codeEndIndex = closeTagMatch.Index;
                parseTextEnd = closeTagMatch.Index + closeTagMatch.Length;
            }
            else if (_info.EnablePreRenderingCodeBlock)
            {
                codeEndIndex = text.Length;
                parseTextEnd = text.Length;
            }
            else
            {
                parseTextBegin = parseTextEnd = -1;
                return null;
            }

            parseTextBegin = match.Index;

            var code = text.Substring(match.Index + match.Length, codeEndIndex - (match.Index + match.Length));
            var lang = match.Groups[2].Value;

            return Convert(lang, code);
        }

        private IEnumerable<Control> Convert(string lang, string code)
        {
            if (string.IsNullOrEmpty(lang))
            {
                var ctxt = new TextBlock
                {
                    Text = code,
                    TextWrapping = TextWrapping.NoWrap
                };
                ctxt.Classes.Add(Markdown.CodeBlockClass);

                var scrl = new ScrollViewer();
                scrl.Classes.Add(Markdown.CodeBlockClass);
                scrl.Content = ctxt;
                scrl.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

                var result = new Border();
                result.Classes.Add(Markdown.CodeBlockClass);
                result.Child = scrl;

                yield return result;
            }
            else
            {
                var txtEdit = new TextEditor
                {
                    Tag = lang,
                    Text = code,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    IsReadOnly = true
                };

                txtEdit.Tag = lang;

                if (RegistryOptions?.GetScopeByLanguageId(lang) is { } scope)
                {
                    var textMate = txtEdit.InstallTextMate(RegistryOptions);
                    textMate.SetGrammar(scope);
                    textMate.SetTheme(CurrentEditorTheme ?? RegistryOptions.GetDefaultTheme());

                    txtEdit.DetachedFromVisualTree += (_, _) => { textMate.Dispose(); };
                }

                var result = new Border();
                result.Classes.Add(Markdown.CodeBlockClass);
                result.Child = txtEdit;

                yield return result;
            }
        }
    }
}