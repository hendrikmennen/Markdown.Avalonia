using Avalonia.Styling;

namespace Markdown.Avalonia.Plugins
{
    public interface IStyleEdit
    {
        void Edit(string styleName, Styles style);
    }
}