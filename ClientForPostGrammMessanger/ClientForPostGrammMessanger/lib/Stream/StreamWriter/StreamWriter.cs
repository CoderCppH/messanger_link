using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientForPostGrammMessanger.lib.Stream.StreamWriter
{
    internal class StreamWriter
    {
        public void WriteBytes(NetworkStream stream, byte[] writeBuffer)
        {
            try
            {
                byte[] sizeData = BitConverter.GetBytes(writeBuffer.Length);
                stream.Write(sizeData, 0, sizeData.Length);
                stream.Write(writeBuffer, 0, writeBuffer.Length);
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error write = $% {e.Message}");
            }
        }
    }
}
