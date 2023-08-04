using Avalonia.Media.Imaging;

namespace Markdown.Avalonia.Utils
{
    public interface IBitmapLoader
    {
        /// <summary>
        /// local file root path or url, the default is current directory.
        /// </summary>
        string AssetPathRoot { set; }

        Bitmap? Get(string urlTxt);
    }
}
