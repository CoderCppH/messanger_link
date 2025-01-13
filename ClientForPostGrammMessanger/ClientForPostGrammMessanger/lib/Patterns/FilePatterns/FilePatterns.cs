namespace ClientForPostGrammMessanger.Patterns.FilePatterns
{
    public class FilePatterns
    {
        public string Command = string.Empty;
        public FileType FileType = FileType.NULL;
        public string FileName = string.Empty;
        public byte[] FileBuffer = default;
        public int FileId;
    }
}
