using System.Net.Sockets;

namespace ResetApiTcp.StreamMessage
{
    internal class StreamReader
    {
        public bool ReadBytes(NetworkStream stream, ref byte[] getbytes)
        {
            Console.WriteLine("read_stream_start");
            bool r = false;
            try
            {
                byte[] sizeData = new byte[4];
                stream.Read(sizeData, 0, sizeData.Length);

                int fileSize = BitConverter.ToInt32(sizeData, 0);
                byte[] fileData = new byte[fileSize];
                int totalBytesRead = 0;
                foreach (byte i_byte in sizeData) 
                {
                    Console.Write($"{i_byte}, ");
                }
                Console.WriteLine();
                Console.WriteLine($"success get size data: {fileSize}");
                while (totalBytesRead < fileSize)
                {
                    //Console.WriteLine($"success get byte: {totalBytesRead}");
                    int bytesRead = stream.Read(fileData, totalBytesRead, fileSize - totalBytesRead);
                    if (bytesRead == 0 )
                    {
                        break;
                    }
                    totalBytesRead += bytesRead;
                }
                getbytes = fileData;
                r = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"error read = %$ {e.Message}");
            }
            Console.WriteLine("read_stream_end");
            return r;
        }
    }
}