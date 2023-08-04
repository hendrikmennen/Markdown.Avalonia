using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaEdit;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AvaloniaEdit.TextMate;
using TextMateSharp.Themes;

namespace Markdown.Avalonia.SyntaxHigh
{
    public class SyntaxSetup
    {
        public static IRawTheme? CurrentEditorTheme;
        public static IAdvancedRegistryOptions? RegistryOptions;

        public IEnumerable<KeyValuePair<string, Func<Match, Control>>> GetOverrideConverters()
        {
            yield return new KeyValuePair<string, Func<Match, Control>>(
                "CodeBlocksWithLangEvaluator",
                CodeBlocksEvaluator);
        }

        private Border CodeBlocksEvaluator(Match match)
        {
            var lang = match.Groups[2].Value;
            var code = match.Groups[3].Value;

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

                return result;
            }
            else
            {
                // check wheither style is set
                if (!ThemeDetector.IsAvalonEditSetup)
                {
                    //SetupStyle();
                }

                var txtEdit = new TextEditor
                {
                    Tag = lang,
                    Text = code,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    IsReadOnly = true
                };

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

                return result;
            }
        }
    }
}
