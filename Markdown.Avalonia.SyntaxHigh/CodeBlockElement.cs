using Avalonia.Controls;
using Avalonia.Layout;
using AvaloniaEdit;
using System;
using System.Collections.Generic;
using Avalonia;
using Markdown.Avalonia.SyntaxHigh.Extensions;
using ColorDocument.Avalonia;
using System.Text;
using AvaloniaEdit.TextMate;
using TextMateSharp.Themes;

namespace Markdown.Avalonia.SyntaxHigh
{
    public class CodeBlockElement : DocumentElement
    {
        public static IRawTheme? CurrentEditorTheme;
        public static IAdvancedRegistryOptions? RegistryOptions;
        
        private SyntaxHighlightProvider _provider;
        private string _code;
        private Lazy<Border> _control;

        public override Control Control => _control.Value;

        public override IEnumerable<DocumentElement> Children => Array.Empty<DocumentElement>();

        public CodeBlockElement(SyntaxHighlightProvider provider, string lang, string code)
        {
            _provider = provider;
            _code = code;
            _control = new Lazy<Border>(() => Create(lang, code));
        }

        public override void ConstructSelectedText(StringBuilder stringBuilder)
        {
            stringBuilder.Append(_code);
        }

        public override void Select(Point from, Point to)
        {
            Helper?.Register(Control);
        }

        public override void UnSelect()
        {
            Helper?.Unregister(Control);
        }

        private Border Create(string lang, string code)
        {
            var langLabel = new Label() { Content = lang };
            langLabel.Classes.Add("LangInfo");

            var copyButton = new Button() { Content = new TextBlock() };
            copyButton.Classes.Add("CopyButton");

            var txtEdit = new TextEditor
            {
                Tag = lang,
                Text = code,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                IsReadOnly = true
            };

            copyButton.Click += (s, e) =>
            {
                var clipboard = TopLevel.GetTopLevel(txtEdit)?.Clipboard;
                clipboard?.SetTextAsync(txtEdit.Text);
            };
            
            if (RegistryOptions?.GetScopeByLanguageId(lang) is { } scope)
            {
                var textMate = txtEdit.InstallTextMate(RegistryOptions);
                textMate.SetGrammar(scope);
                textMate.SetTheme(CurrentEditorTheme ?? RegistryOptions.GetDefaultTheme());

                txtEdit.DetachedFromVisualTree += (_, _) => { textMate.Dispose(); };
            }

            var cdPd = new CodePad();
            cdPd.Content = txtEdit;
            cdPd.ExandableMenu = copyButton;
            cdPd.AlwaysShowMenu = langLabel;

            var result = new Border();
            result.Classes.Add(Markdown.CodeBlockClass);
            result.Child = cdPd;

            return result;
        }
    }
}
