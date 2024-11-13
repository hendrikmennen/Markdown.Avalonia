using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace Markdown.Avalonia.Extensions
{
    public class MultiplyExtension : MarkupExtension
    {
        private readonly string _resourceKey;
        private readonly double _scale;

        public MultiplyExtension(string resourceKey) : this(resourceKey, 1)
        {
        }

        public MultiplyExtension(string resourceKey, double scale)
        {
            _resourceKey = resourceKey;
            _scale = scale;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dyExt = new DynamicResourceExtension(_resourceKey);

            var brush = dyExt.ProvideValue(serviceProvider);

            return new MultiBinding
            {
                Bindings = new[] { brush },
                Converter = new MultiplyConverter(_scale)
            };
        }

        private class MultiplyConverter : IMultiValueConverter
        {
            public MultiplyConverter(double scale)
            {
                Scale = scale;
            }

            public double Scale { get; }

            public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
            {
                return values[0] switch
                {
                    short s => (short)(s * Scale),
                    int i => (int)(i * Scale),
                    long l => (long)(l * Scale),
                    float f => (float)(f * Scale),
                    double d => d * Scale,
                    _ => values[0]
                };
            }
        }
    }
}