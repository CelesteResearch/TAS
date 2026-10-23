namespace Celeste.Mods.Tas.Input
{
    /// /// Represents a commented line in a TAS file
    public readonly record struct SourceTasComment
    {
        public readonly int Frame;

        public readonly string FilePath;
        public readonly int FileLine;
        public readonly int StudioLine;

        internal SourceTasSourceLocation Source => new(FilePath, FileLine, StudioLine);

        public readonly string Text;

        public SourceTasComment(int frame, string filePath, int fileLine, int studioLine, string text)
        {
            Frame = frame;
            FilePath = filePath;
            FileLine = fileLine;
            StudioLine = studioLine;
            Text = string.IsNullOrEmpty(text)
                ? string.Empty
                : text["#".Length..].Trim();
        }
    }
}
