using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForPostGrammMessanger.lib.EndPointer
{
    public class EndPointer
    {
        private string _host;
        private int _port;
        public EndPointer(string host, int port) {
            this._host = host;
            this._port = port;
        }
        public string GetHost() 
        {
            return _host;
        }
        public int GetPort() 
        {
            return _port;
        }
    }
}
