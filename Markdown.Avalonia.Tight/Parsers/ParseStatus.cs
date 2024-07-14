namespace Markdown.Avalonia.Parsers
{
    public struct ParseStatus
    {
        public static readonly ParseStatus Init = new(true);

        public bool SupportTextAlignment { get; }

        public ParseStatus(bool supportTextAlignment)
        {
            SupportTextAlignment = supportTextAlignment;
        }
    }
}