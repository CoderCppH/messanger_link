using Newtonsoft.Json;
using System.Text;

namespace ResetApiTcp.Convert.ConvertJson
{
    public static class ConvertJson
    {
        public static byte[] Object_in_json_in_buffer<T>(T obj)
        {
            byte[] buffer = null;
            string json = JsonConvert.SerializeObject(obj);
            buffer = Encoding.UTF8.GetBytes(json);
            return buffer;
        }

        public static T buffer_in_string_json<T>(byte[] data)
        {
            T obj;
            string json = Encoding.UTF8.GetString(data);
            obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
