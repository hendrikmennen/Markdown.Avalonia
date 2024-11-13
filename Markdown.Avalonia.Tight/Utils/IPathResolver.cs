using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Markdown.Avalonia.Utils
{
    /// <summary>
    ///     Derived classes open an image resources from relative or absolute paths.
    ///     Relative paths may be converted to absolute paths in derived classes.
    /// </summary>
    public interface IPathResolver
    {
        string? AssetPathRoot { set; }
        IEnumerable<string>? CallerAssemblyNames { set; }

        /// <summary>
        ///     Open an image resource.
        /// </summary>
        /// <param name="relativeOrAbsolutePath">The image path</param>
        /// <returns>
        ///     The task of the image resource.
        ///     Returns null if the image does not exist or cannot be opened.
        ///     If the result of the asynchronous search cannot be resolved, Task.Result returns null.
        /// </returns>
        Task<Stream?>? ResolveImageResource(string relativeOrAbsolutePath);
    }
}