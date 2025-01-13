using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResetApiTcp.MailRu
{
    public class GeneratorCode
    {
        private string _alpha_code = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
        public string NextCode(int sizeCode) 
        {
            Random rand = new Random();
            string code = "";
            for (int i = 0; i < sizeCode; i++) 
            {
                code += _alpha_code[rand.Next(0, _alpha_code.Length - 1)];
            }
            return code;
        }
    }
}
