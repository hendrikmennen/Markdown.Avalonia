using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using ColorTextBlock.Avalonia.Geometries;

namespace ColorTextBlock.Avalonia
{
    /// <summary>
    ///     Hyperlink decoration
    /// </summary>
    public class CHyperlink : CSpan
    {
        /// <summary>
        ///     Background brush during mouse hover
        /// </summary>
        /// <seealso cref="HoverBackground" />
        public static readonly StyledProperty<IBrush?> HoverBackgroundProperty =
            AvaloniaProperty.Register<CHyperlink, IBrush?>(nameof(Foreground));

        /// <summary>
        ///     Foreground brush during mouse hover
        /// </summary>
        /// <seealso cref="HoverForeground" />
        public static readonly StyledProperty<IBrush?> HoverForegroundProperty =
            AvaloniaProperty.Register<CHyperlink, IBrush?>(nameof(Foreground));

        public CHyperlink()
        {
        }

        public CHyperlink(IEnumerable<CInline> inlines) : base(inlines)
        {
        }

        /// <summary>
        ///     Background brush during mouse hover
        /// </summary>
        public IBrush? HoverBackground
        {
            get => GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }

        /// <summary>
        ///     Foreground brush during mouse hover
        /// </summary>
        public IBrush? HoverForeground
        {
            get => GetValue(HoverForegroundProperty);
            set => SetValue(HoverForegroundProperty, value);
        }

        /// <summary>
        ///     Link click action
        /// </summary>
        public Action<string>? Command { get; set; }

        /// <summary>
        ///     Link click action parameter
        /// </summary>
        public string? CommandParameter { get; set; }


        protected override IEnumerable<CGeometry> MeasureOverride(
            double entireWidth,
            double remainWidth)
        {
            var metrics = base.MeasureOverride(
                entireWidth,
                remainWidth);

            foreach (var metry in metrics)
            {
                metry.OnClick = ctrl => Command?.Invoke(CommandParameter ?? string.Empty);

                metry.OnMousePressed = ctrl => { PseudoClasses.Add(":pressed"); };

                metry.OnMouseReleased = ctrl => { PseudoClasses.Remove(":pressed"); };

                metry.OnMouseEnter = ctrl =>
                {
                    PseudoClasses.Add(":pointerover");
                    PseudoClasses.Add(":hover");

                    try
                    {
                        ctrl.Cursor = new Cursor(StandardCursorType.Hand);
                    }
                    catch
                    {
                        /*I cannot assume Cursor.ctor doesn't throw an exception.*/
                    }

                    var tmetries =
                        metry is DecoratorGeometry d ? d.Targets.OfType<TextGeometry>() :
                        metry is TextGeometry t ? new[] { t } :
                        new TextGeometry[0];

                    if (tmetries != null)
                    {
                        foreach (var tmetry in tmetries)
                        {
                            tmetry.TemporaryForeground = HoverForeground;
                            tmetry.TemporaryBackground = HoverBackground;
                        }

                        RequestRender();
                    }
                };

                metry.OnMouseLeave = ctrl =>
                {
                    PseudoClasses.Remove(":pointerover");
                    PseudoClasses.Remove(":hover");

                    ctrl.Cursor = Cursor.Default;

                    var tmetries =
                        metry is DecoratorGeometry d ? d.Targets.OfType<TextGeometry>() :
                        metry is TextGeometry t ? new[] { t } :
                        new TextGeometry[0];

                    if (tmetries != null)
                    {
                        foreach (var tmetry in tmetries)
                        {
                            tmetry.TemporaryForeground = null;
                            tmetry.TemporaryBackground = null;
                        }

                        RequestRender();
                    }
                };

                yield return metry;
            }
        }
    }
}