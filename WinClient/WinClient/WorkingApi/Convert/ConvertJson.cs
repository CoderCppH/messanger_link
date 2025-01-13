using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinClient.WorkingApi.Convert
{
    public static class ConvertJson
    {
        public static byte[]  Object_in_json_in_buffer<T>(T obj) 
        {
            byte[] buffer = null;
            if (obj != null) {
                string json = JsonConvert.SerializeObject(obj);
                buffer = Encoding.UTF8.GetBytes(json);
            }
            return buffer;
        }

        public static T buffer_in_string_json<T>(byte[] data)
        {
            T obj;
            try
            {
                string json = Encoding.UTF8.GetString(data);
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                obj = default(T);
            }
            return obj;
        }
    }
}
