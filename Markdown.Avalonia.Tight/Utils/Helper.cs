using System;

namespace Markdown.Avalonia.Utils
{
    internal static class Helper
    {
        public static void ThrowInvalidOperation(string msg)
        {
            throw new InvalidOperationException(msg);
        }
    }
}