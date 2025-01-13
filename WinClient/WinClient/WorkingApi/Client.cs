using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi
{
    class Client
    {
        private WinClient.WorkingApi.StreamMessage.StreamMessage.StreamReader _streamRead;
        private WinClient.WorkingApi.StreamMessage.StreamMessage.StreamWriter _streamWrite;
        private TcpClient _tcpClient;
        private string host;
        private int port;
        bool _error_client = false;
        public Client(string host, int port) 
        {
            this.host = host;
            this.port = port;
        }
        public void Connect() 
        {
            try
            {
                _tcpClient = new TcpClient(host, port);
                _streamRead = new WinClient.WorkingApi.StreamMessage.StreamMessage.StreamReader();
                _streamWrite = new WinClient.WorkingApi.StreamMessage.StreamMessage.StreamWriter();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error Client Tcp: {ex.Message}");
                _error_client = true;
            }
        }
        public bool Read(ref byte[] data) 
        {
            if(!_error_client)
                return _streamRead.ReadBytes(_tcpClient.GetStream(), ref data);
            else 
                return false;
        }
        public void Write(byte[] data) 
        {
            if (!_error_client)
                _streamWrite.WriteBytes(_tcpClient.GetStream(), data);
        }
        public void Close() 
        {
            if (_tcpClient != null) 
                _tcpClient.Close();
        }
    }
}
