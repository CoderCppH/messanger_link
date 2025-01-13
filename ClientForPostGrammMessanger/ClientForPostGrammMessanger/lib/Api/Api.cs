using ClientForPostGrammMessanger.lib.Config.Patterns;
using ClientForPostGrammMessanger.patterns;
using ClientForPostGrammMessanger.Patterns.FilePatterns;
using ClientForPostGrammMessanger.Patterns.TransportCommand;
using System.Net.Sockets;

namespace ClientForPostGrammMessanger.lib.Api
{
    public class Api
    {
        private Client client = new Client();
        private Config.Config config = new Config.Config();
        private NetworkStream stream;
        private Stream.StreamReader.StreanReader streamReader = new Stream.StreamReader.StreanReader();
        private Stream.StreamWriter.StreamWriter streamWriter = new Stream.StreamWriter.StreamWriter();

        private void ConnectClient() 
        {
            p_config _p_config = config.GetConfig();
            client.Connect(new EndPointer.EndPointer(_p_config.host, _p_config.port));
            Console.WriteLine($"client connected to {_p_config.host}:{_p_config.port}");
            InitStream();
        }
        private void InitStream() 
        {
            stream = client.GetStream();
        }
        public void AddPhoto(byte[] buffer, string name) 
        {
           ConnectClient();
            p_dtf SET = new p_dtf();
            p_dtf GET = new p_dtf();
            FilePatterns p_file = new FilePatterns();

            p_file.Command = "image_add";
            p_file.FileBuffer = buffer;
            p_file.FileName = name;
            p_file.FileType = FileType.Image;
            p_file.FileId = 0;
            //insert
            SET.Data = Convert.ConvertJson.ConvertJson.Object_in_json_in_buffer<FilePatterns>(p_file);
            SET.DataFormat = Patterns.p_fdt.File;
            SET.NameUser = Environment.MachineName;
            SET.Request = Patterns.p_rt.POST;
            streamWriter.WriteBytes(stream, Convert.ConvertJson.ConvertJson.Object_in_json_in_buffer<p_dtf>(SET));
            byte[] get_buffer = new byte[1];
            streamReader.ReadBytes(stream, ref get_buffer);
            GET = Convert.ConvertJson.ConvertJson.buffer_in_string_json<p_dtf>(get_buffer);

            Console.WriteLine(Convert.ConvertJson.ConvertJson.buffer_in_string_json<string>(GET.Data));
        }
    }
}
