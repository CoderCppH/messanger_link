using System.Net.Sockets;
namespace ResetApiTcp.StreamMessage
{
    internal class StreamWriter
    {
        public void WriteBytes(NetworkStream stream, byte[] writeBuffer)
        {
            Console.WriteLine("write_stream_start");
            try
            {

                byte[] sizeData = BitConverter.GetBytes(writeBuffer.Length);
                Console.WriteLine($"write_size_sending_data:= [{sizeData[0]}, {sizeData[1]}, {sizeData[2]}, {sizeData[3]}]");
                stream.Write(sizeData, 0, sizeData.Length);
                stream.Write(writeBuffer, 0, writeBuffer.Length);
                stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine($"error write = $% {e.Message}");
            }
            Console.WriteLine("write_stream_end");
        }
    }
}