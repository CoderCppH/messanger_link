using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi.Patterns.TransportCommand
{
    class TransportCommand
    {
        public string Command = string.Empty;
        public byte[] ExData = new byte[byte.MinValue];
    }
}
