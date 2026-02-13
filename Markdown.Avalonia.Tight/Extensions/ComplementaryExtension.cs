using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Markdown.Avalonia.Extensions
{
    public class ComplementaryExtension : MarkupExtension
    {
        private readonly string _brushName;

        public ComplementaryExtension(string colorKey)
        {
            this._brushName = colorKey;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dyExt = new DynamicResourceExtension(_brushName);

            var brush = dyExt.ProvideValue(serviceProvider);

            return new MultiBinding()
            {
                Bindings = new IBinding[] { brush },
                Converter = new ComplementaryConverter()
            };
        }

        class ComplementaryConverter : IMultiValueConverter
        {
            public ComplementaryConverter() { }

            public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
            {
                if (values.Count < 1 || values[0] is null || values[0] == AvaloniaProperty.UnsetValue)
                    return AvaloniaProperty.UnsetValue;

                Color c;
                if (values[0] is ISolidColorBrush b)
                    c = b.Color;
                else if (values[0] is Color col)
                    c = col;
                else if (values[0] is IBrush brush)
                    return brush;
                else
                    return AvaloniaProperty.UnsetValue;


                var rgb = new int[] { c.R, c.G, c.B };
                var s = rgb.Max() + rgb.Min();

                return new SolidColorBrush(
                            Color.FromArgb(
                                c.A,
                                (byte)(s - c.R),
                                (byte)(s - c.G),
                                (byte)(s - c.B)));
            }
        }
    }
}
