using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Markdown.Avalonia.Extensions
{
    public class DivideColorExtension : MarkupExtension
    {
        private readonly string _frmKey;
        private readonly string _toKey;
        private readonly double _relate;

        public DivideColorExtension(string frm, string to, double relate)
        {
            this._frmKey = frm;
            this._toKey = to;
            this._relate = relate;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var left = CreateBinding(_frmKey, serviceProvider);
            var right = CreateBinding(_toKey, serviceProvider);

            return new MultiBinding()
            {
                Bindings = new IBinding[] { left, right },
                Converter = new DivideConverter(_relate)
            };
        }

        private static IBinding CreateBinding(string key, IServiceProvider serviceProvider)
        {
            if (Color.TryParse(key, out var color))
                return new Binding { Source = color, Mode = BindingMode.OneWay };

            var ext = new DynamicResourceExtension(key);
            var provided = ext.ProvideValue(serviceProvider);

            if (provided is IBinding binding)
                return binding;

            return new Binding { Source = provided, Mode = BindingMode.OneWay };
        }
    }

    class DivideConverter : IMultiValueConverter
    {
        public double Relate { get; }

        public DivideConverter(double relate)
        {
            Relate = relate;
        }

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            Color colL;
            if (values[0] is ISolidColorBrush bl)
                colL = bl.Color;
            else if (values[0] is Color cl)
                colL = cl;
            else
                return AvaloniaProperty.UnsetValue;

            Color colR;
            if (values[1] is ISolidColorBrush br)
                colR = br.Color;
            else if (values[1] is Color cr)
                colR = cr;
            else
                return AvaloniaProperty.UnsetValue;

            static byte Calc(byte l, byte r, double d)
                => (byte)(l * (1 - d) + r * d);

            return new SolidColorBrush(
                        Color.FromArgb(
                            Calc(colL.A, colR.A, Relate),
                            Calc(colL.R, colR.R, Relate),
                            Calc(colL.G, colR.G, Relate),
                            Calc(colL.B, colR.B, Relate)));
        }
    }
}
