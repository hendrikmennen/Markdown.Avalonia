using System.Collections.ObjectModel;
using Avalonia.Metadata;
using Markdown.Avalonia.Plugins;

namespace Markdown.Avalonia.SyntaxHigh
{
    public class SyntaxHighlight : IMdAvPlugin
    {
        [Content] public ObservableCollection<Alias> Aliases { get; } = new();

        public void Setup(SetupInfo info)
        {
            info.Register(new SyntaxOverride(Aliases, info));
            info.Register(new StyleEdit());
        }
    }
}