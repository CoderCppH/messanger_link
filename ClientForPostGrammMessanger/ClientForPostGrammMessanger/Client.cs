using ClientForPostGrammMessanger.lib.EndPointer;
using System.Net.Sockets;

namespace ClientForPostGrammMessanger
{
    public class Client
    {
        TcpClient _client = default;
        NetworkStream _stream = default;
        EndPointer _end_pointer = default;
        public Client() 
        {
            _client = null;
            _stream = null;
        }
        public Client(EndPointer endPointer) 
        {
            Connect(endPointer);
        }
        public EndPointer GetEndPointer() 
        {
            return _end_pointer;
        }
        public NetworkStream GetStream() 
        {
            return _stream;
        }
        public void Connect(EndPointer endPointer) 
        {
            if (_client == null)
            {
                _client = new TcpClient(endPointer.GetHost(), endPointer.GetPort());
                _stream = _client.GetStream();
            }
            else 
                if (_client.Connected) 
                    _client.Close();
        }
        
    }
}
