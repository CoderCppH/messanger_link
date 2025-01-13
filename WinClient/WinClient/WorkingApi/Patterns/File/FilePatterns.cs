using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi.Patterns.File
{
    class FilePatterns
    {
        public string Command = string.Empty;
        public FileType FileType = FileType.NULL;
        public string FileName = string.Empty;
        public byte[] Image;
        public int FileId;
    }
}
