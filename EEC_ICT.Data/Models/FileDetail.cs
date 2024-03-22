namespace EEC_ICT.Data.Models
{
    public class FileDetail
    {
        // Constructor File:
        // FullPath = Directory + File
        // Directory = CurrentServer + Path
        // File = FileName + FileExtension
        public string Path { get; set; }
        public string File { get; set; } // File = FileName + FileExtension
        public string OriginName { get; set; }

    }
}