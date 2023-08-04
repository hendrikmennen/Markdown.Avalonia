using System;

namespace Markdown.Avalonia.Utils
{
    static class Helper
    {
        public static void ThrowInvalidOperation(string msg)
        {
            throw new InvalidOperationException(msg);
        }
    }
}
