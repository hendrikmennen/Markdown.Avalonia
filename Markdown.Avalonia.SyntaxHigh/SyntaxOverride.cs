using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaEdit;
using Markdown.Avalonia.Plugins;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Avalonia;
using System.Collections.ObjectModel;
using Markdown.Avalonia.SyntaxHigh.Extensions;
using Markdown.Avalonia.Parsers;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Avalonia.Metadata;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;
using TextMateSharp.Themes;

namespace Markdown.Avalonia.SyntaxHigh
{
    public class SyntaxOverride : IBlockOverride
    {
        private SyntaxHighlightProvider _provider;
        private SetupInfo _info;
        
        public static IRawTheme? CurrentEditorTheme;
        public static IAdvancedRegistryOptions? RegistryOptions;
        public string ParserName => "CodeBlocksWithLangEvaluator";


        public SyntaxOverride(ObservableCollection<Alias> aliases, SetupInfo info)
        {
            _provider = new SyntaxHighlightProvider(aliases);
            _info = info;
        }

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

            string code = text.Substring(match.Index + match.Length, codeEndIndex - (match.Index + match.Length));
            string lang = match.Groups[2].Value;

            return Convert(lang, code);
        }

        private IEnumerable<Control> Convert(string lang, string code)
        {
            if (String.IsNullOrEmpty(lang))
            {
                var ctxt = new TextBlock()
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
                
                    txtEdit.DetachedFromVisualTree += (_, _) =>
                    {
                        textMate.Dispose();
                    };
                }

                var result = new Border();
                result.Classes.Add(Markdown.CodeBlockClass);
                result.Child = txtEdit;

                yield return result;
            }
        }
    }
}
