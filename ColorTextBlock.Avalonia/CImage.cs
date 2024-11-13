using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using ColorTextBlock.Avalonia.Geometries;

namespace ColorTextBlock.Avalonia
{
    /// <summary>
    ///     Displays an image
    /// </summary>
    public class CImage : CInline
    {
        public static readonly StyledProperty<double?> LayoutWidthProperty =
            AvaloniaProperty.Register<CImage, double?>(nameof(LayoutWidth));

        public static readonly StyledProperty<double?> LayoutHeightProperty =
            AvaloniaProperty.Register<CImage, double?>(nameof(LayoutHeight));

        public static readonly StyledProperty<double?> RelativeWidthProperty =
            AvaloniaProperty.Register<CImage, double?>(nameof(RelativeWidth));

        /// <summary>
        ///     Determine wheither image auto fitting or protrude outside Control
        ///     when image is too width to be rendered in control.
        ///     If you set 'true', Image is fitted to control width.
        /// </summary>
        public static readonly StyledProperty<bool> FittingWhenProtrudeProperty =
            AvaloniaProperty.Register<CImage, bool>(nameof(FittingWhenProtrude), true);

        /// <summary>
        ///     Save aspect ratio if one of <see cref="LayoutHeightProperty" /> or <see cref="LayoutWidthProperty" /> set.
        /// </summary>
        public static readonly StyledProperty<bool> SaveAspectRatioProperty =
            AvaloniaProperty.Register<CImage, bool>(nameof(SaveAspectRatio));

        public CImage(Task<IImage?> task, IImage whenError)
        {
            if (task is null) throw new NullReferenceException(nameof(task));
            if (whenError is null) throw new NullReferenceException(nameof(whenError));

            Task = task;
            WhenError = whenError;
        }

        public CImage(IImage image)
        {
            if (image is null) throw new NullReferenceException(nameof(image));
            WhenError = Image = image;
        }

        public double? LayoutWidth
        {
            get => GetValue(LayoutWidthProperty);
            set => SetValue(LayoutWidthProperty, value);
        }

        public double? LayoutHeight
        {
            get => GetValue(LayoutHeightProperty);
            set => SetValue(LayoutHeightProperty, value);
        }

        public double? RelativeWidth
        {
            get => GetValue(RelativeWidthProperty);
            set => SetValue(RelativeWidthProperty, value);
        }

        public bool FittingWhenProtrude
        {
            get => GetValue(FittingWhenProtrudeProperty);
            set => SetValue(FittingWhenProtrudeProperty, value);
        }

        public bool SaveAspectRatio
        {
            get => GetValue(SaveAspectRatioProperty);
            set => SetValue(SaveAspectRatioProperty, value);
        }

        public Task<IImage?>? Task { get; }
        private IImage WhenError { get; }
        public IImage? Image { private set; get; }

        protected override IEnumerable<CGeometry> MeasureOverride(
            double entireWidth, double remainWidth)
        {
            if (Image is null)
            {
                if (Task is null)
                {
                    Image = WhenError;
                }
                else if (
                    Task.Status == TaskStatus.RanToCompletion
                    || Task.Status == TaskStatus.Faulted
                    || Task.Status == TaskStatus.Canceled)
                {
                    Image = Task.IsFaulted ? WhenError : Task.Result ?? WhenError;
                }
                else
                {
                    Image = new WriteableBitmap(
                        new PixelSize(1, 1),
                        new Vector(96, 96),
                        PixelFormat.Rgb565,
                        AlphaFormat.Premul);

                    Thread.MemoryBarrier();

                    System.Threading.Tasks.Task.Run(() =>
                    {
                        Task.Wait();
                        Dispatcher.UIThread.InvokeAsync(() =>
                        {
                            Image = Task.IsFaulted ? WhenError : Task.Result ?? WhenError;
                            RequestMeasure();
                        });
                    });
                }
            }

            var imageWidth = Image.Size.Width;
            var imageHeight = Image.Size.Height;

            if (RelativeWidth.HasValue)
            {
                var aspect = imageHeight / imageWidth;
                imageWidth = RelativeWidth.Value * entireWidth;
                imageHeight = aspect * imageWidth;
            }

            if (LayoutWidth.HasValue)
            {
                imageWidth = LayoutWidth.Value;
                if (SaveAspectRatio && !LayoutHeight.HasValue)
                {
                    var aspect = Image.Size.Height / Image.Size.Width;
                    imageHeight = aspect * imageWidth;
                }
            }

            if (LayoutHeight.HasValue)
            {
                imageHeight = LayoutHeight.Value;
                if (SaveAspectRatio && !LayoutWidth.HasValue)
                {
                    var aspect = Image.Size.Width / Image.Size.Height;
                    imageWidth = aspect * imageHeight;
                }
            }

            if (imageWidth > remainWidth)
            {
                if (entireWidth != remainWidth) yield return new LineBreakMarkGeometry(this);

                if (FittingWhenProtrude && imageWidth > entireWidth)
                {
                    var aspect = imageHeight / imageWidth;
                    imageWidth = entireWidth;
                    imageHeight = aspect * imageWidth;
                }
            }

            yield return new ImageGeometry(Image, imageWidth, imageHeight,
                TextVerticalAlignment);
        }

        public override string AsString()
        {
            return " $$Image$$ ";
        }
    }
}