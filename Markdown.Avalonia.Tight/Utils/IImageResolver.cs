using System.IO;
using System.Threading.Tasks;
using Avalonia.Media;

namespace Markdown.Avalonia.Utils
{
    /// <summary>
    ///     Derivered classes create IImage from image resources.
    /// </summary>
    public interface IImageResolver
    {
        /// <summary>
        ///     Open image
        /// </summary>
        /// <param name="stream">image resource. Stream is seek-able and close-able.</param>
        /// <returns>
        ///     Images.
        ///     Return null if derived class cannot create IImage, e.g. because resource is not supportted.
        /// </returns>
        Task<IImage?> Load(Stream stream);
    }
}