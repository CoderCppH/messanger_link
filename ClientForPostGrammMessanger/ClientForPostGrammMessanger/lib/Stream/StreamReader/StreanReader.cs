using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientForPostGrammMessanger.lib.Stream.StreamReader
{
    internal class StreanReader
    {
        public bool ReadBytes(NetworkStream stream, ref byte[] getbytes)
        {
            bool r = false;
            try
            {
                byte[] sizeData = new byte[4];
                stream.Read(sizeData, 0, sizeData.Length);
                int fileSize = BitConverter.ToInt32(sizeData, 0);
                byte[] fileData = new byte[fileSize];
                int totalBytesRead = 0;

                while (totalBytesRead < fileSize)
                {
                    int bytesRead = stream.Read(fileData, totalBytesRead, fileSize - totalBytesRead);
                    if (bytesRead == 0)
                        break;
                    
                    totalBytesRead += bytesRead;
                }
                getbytes = fileData;
                r = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"error read = %$ {e.Message}");
            }
            return r;
        }
    }
}
