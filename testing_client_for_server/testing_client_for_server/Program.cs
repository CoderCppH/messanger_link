using Newtonsoft.Json;

namespace std 
{
    class Pro 
    {
        static void Main() 
        {
            
            Console.WriteLine("Start");
            ClientForPostGrammMessanger.lib.Api.Api api = new ClientForPostGrammMessanger.lib.Api.Api();
            api.AddPhoto(File.ReadAllBytes("1.jpg"), "File_number_1");
        }
    }
}