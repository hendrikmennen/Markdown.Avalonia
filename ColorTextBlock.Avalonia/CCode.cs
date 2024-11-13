using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using ColorTextBlock.Avalonia.Fonts;

namespace ColorTextBlock.Avalonia
{
    /// <summary>
    ///     Monospace decoration
    /// </summary>
    public class CCode : CSpan
    {
        /// <summary>
        ///     Monospace font family used for code display.
        /// </summary>
        /// <see cref="MonospaceFontFamily" />
        public static readonly StyledProperty<FontFamily> MonospaceFontFamilyProperty =
            AvaloniaProperty.Register<CCode, FontFamily>(
                nameof(MonospaceFontFamily),
                FontFamilyCollector.TryGetMonospace() ?? FontFamily.Default,
                true);

        public CCode()
        {
            var obsvr = this.GetBindingObservable(MonospaceFontFamilyProperty);
            Bind(FontFamilyProperty, obsvr);
        }

        public CCode(IEnumerable<CInline> inlines) : base(inlines)
        {
            var obsvr = this.GetBindingObservable(MonospaceFontFamilyProperty);
            Bind(FontFamilyProperty, obsvr);
        }

        /// <summary>
        ///     Monospace font family used for code display.
        /// </summary>
        public FontFamily MonospaceFontFamily
        {
            get => GetValue(MonospaceFontFamilyProperty);
            set => SetValue(MonospaceFontFamilyProperty, value);
        }
    }
}