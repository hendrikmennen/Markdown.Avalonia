using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;

namespace Markdown.Avalonia.Extensions
{
    public class AlphaExtension : MarkupExtension
    {
        private readonly float _alpha;
        private readonly string _brushName;

        public AlphaExtension(string colorKey) : this(colorKey, 1f)
        {
        }

        public AlphaExtension(string colorKey, float alpha)
        {
            _brushName = colorKey;
            _alpha = alpha;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dyExt = new DynamicResourceExtension(_brushName);

            var brush = dyExt.ProvideValue(serviceProvider);

            return new MultiBinding
            {
                Bindings = new[] { brush },
                Converter = new AlphaConverter(_alpha)
            };
        }

        private class AlphaConverter : IMultiValueConverter
        {
            public AlphaConverter(float alpha)
            {
                Alpha = alpha;
            }

            public float Alpha { get; }

            public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
            {
                Color c;
                if (values[0] is ISolidColorBrush b)
                    c = b.Color;
                else if (values[0] is Color col)
                    c = col;
                else
                    return values[0];

                return new SolidColorBrush(
                    Color.FromArgb(
                        (byte)(c.A / 255f * Alpha * 255f),
                        c.R, c.G, c.B));
            }
        }
    }
}