using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi.StreamMessage.StreamMessage
{
    internal class StreamWriter
    {

        /* public void WriteBytes(NetworkStream stream, byte[] writeBuffer)
         {
             try
             {
                 stream.Write(writeBuffer, 0, writeBuffer.Length);
                 stream.Flush();
             }
             catch (Exception e)
             {
                 MessageBox.Show($"error write = $% {e.Message}");
             }
         }*/
        public void WriteBytes(NetworkStream stream, byte[] writeBuffer)
        {
            try
            {
                // Отправка размера файла
                byte[] sizeData = BitConverter.GetBytes(writeBuffer.Length);
                stream.Write(sizeData, 0, sizeData.Length);

                // Отправка файла
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
