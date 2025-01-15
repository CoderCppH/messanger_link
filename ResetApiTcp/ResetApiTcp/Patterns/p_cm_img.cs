using ResetApiTcp.Cliernt.Command.Sql.SqlCommand.MyExeption.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResetApiTcp.Patterns
{
    public class p_cm_img
    {
        public string command;
        public byte[] image;
        public p_user user = new p_user();
    }
}
