using System;
using System.Collections.Generic;

namespace Markdown.Avalonia.Plugins
{
    public interface IMdAvPlugin
    {
        void Setup(SetupInfo info);
    }

    public interface IMdAvPluginRequestAnother : IMdAvPlugin
    {
        IEnumerable<Type> DependsOn { get; }

        void Inject(IEnumerable<IMdAvPlugin> plugin);
    }
}