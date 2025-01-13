using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi.Patterns.User
{

    class SqlUserType
    {
        public int Id = default(int);
        public string FirstName = String.Empty;
        public string LastName = String.Empty;
        public string Password = String.Empty;
        public long PhoneNumber = default(int);
    }
    
}
