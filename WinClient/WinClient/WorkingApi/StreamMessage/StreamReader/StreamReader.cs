using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi.StreamMessage.StreamMessage
{
    internal class StreamReader
    {

        /* public bool ReadBytes(NetworkStream stream, ref byte[] getbytes, int size_byte = 128)
         {
             bool r = false;
             try
             {
                 byte[] data = new byte[size_byte]; // буфер для получаемых данных
                 StringBuilder builder = new StringBuilder();
                 int bytes = 0;
                 do
                 {
                     bytes = stream.Read(data, 0, data.Length);
                     builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                 }
                 while (stream.DataAvailable);

                 getbytes = Encoding.UTF8.GetBytes(builder.ToString());

             }
             catch (Exception e)
             {
                 MessageBox.Show($"error write = $% {e.Message}");
             }
             return r;
         }*/
        public bool ReadBytes(NetworkStream stream, ref byte[] getbytes)
        {
            bool r = false;
            try
            {
                byte[] sizeData = new byte[4];
                stream.Read(sizeData, 0, sizeData.Length);
                int fileSize = BitConverter.ToInt32(sizeData, 0);

                // Чтение файла
                byte[] fileData = new byte[fileSize];
                int totalBytesRead = 0;

                while (totalBytesRead < fileSize)
                {
                    int bytesRead = stream.Read(fileData, totalBytesRead, fileSize - totalBytesRead);
                    if (bytesRead == 0)
                    {
                        break; // Соединение закрыто
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
            return r;
        }
    }
}
