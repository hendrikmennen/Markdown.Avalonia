namespace Markdown.Avalonia.Parsers
{
    public struct ParseStatus
    {
        public static readonly ParseStatus Init = new ParseStatus(true);

        public bool SupportTextAlignment { get; }

        public ParseStatus(bool supportTextAlignment)
        {
            SupportTextAlignment = supportTextAlignment;
        }
    }
}
