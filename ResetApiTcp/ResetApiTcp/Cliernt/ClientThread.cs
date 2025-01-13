using Newtonsoft.Json;
using ResetApiTcp.Cliernt.Command.Sql.DataBase;
using ResetApiTcp.Cliernt.Command.Sql.SqlCommand;
using ResetApiTcp.Cliernt.Command.Sql.SqlCommand.MyExeption.Users;
using ResetApiTcp.Convert.ConvertJson;
using ResetApiTcp.patterns;
using ResetApiTcp.Patterns;
using ResetApiTcp.Patterns.FilePatterns;
using ResetApiTcp.Patterns.TransportCommand;
using System.Net.Sockets;
using System.Text;

namespace ResetApiTcp.Cliernt
{
    internal class ClientThread
    {
        private SQLiteDataBase DataBaseSql; 
        private Storage.StorageImage StorageImage;
        private SQLiteCommands Commands;
        private TcpClient Client;
        public ClientThread(TcpClient Client) 
        {
            this.Client = Client;
            NetworkStream stream = Client.GetStream();
            StreamMessage.StreamReader read = new StreamMessage.StreamReader();
            StreamMessage.StreamWriter write = new StreamMessage.StreamWriter();
            p_dtf GET = null;
            p_dtf SET = null;
            DataBaseSql = new SQLiteDataBase(GeneralMeaning.GeneralMeaning.DataBaseName);
            //Open Db
            DataBaseSql.Open();
            Commands = new SQLiteCommands(DataBaseSql);
            StorageImage = new Storage.StorageImage(DataBaseSql);
            try
            {
                byte[] bufferRead = new byte[128];
                read.ReadBytes(stream, ref bufferRead);
                string read_text = Encoding.UTF8.GetString(bufferRead);
                Console.WriteLine($"READ: {read_text}");
                GET = JsonConvert.DeserializeObject<p_dtf>(read_text);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error read format: {e.Message}");
            }

            SET = Update(GET);
            try
            {
                string write_text = JsonConvert.SerializeObject(SET);
                Console.WriteLine($"WRITE: {write_text}");
                byte[] bufferWrite = Encoding.UTF8.GetBytes(write_text);
                write.WriteBytes(stream, bufferWrite);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error write format: {e.Message}");
            }

            Commands.Close();

        }
        private p_dtf Update(p_dtf GET) 
        {
            if (GET != null)
            {
                p_dtf SET = new p_dtf();
                SET.NameUser = Environment.MachineName;
                SET.Request = GET.Request;
                SET.DataFormat = GET.DataFormat;

                //read data and write data
                switch (GET.Request)
                {
                    case p_rt.GET:
                        {
                            if (GET.DataFormat.Equals(p_fdt.Json))
                            {

                            }
                            else if (GET.DataFormat.Equals(p_fdt.File))
                            {
                                
                                FilePatterns fileInfo = ConvertJson.buffer_in_string_json<FilePatterns>(GET.Data);
                                if (fileInfo.FileType == FileType.Image) {
                                    //StorageImage.Add("Name1", File.ReadAllBytes("./devushka_ozero_led_1067884_3840x2400.jpg"));
                                    switch (fileInfo.Command)
                                    {
                                        case "image_get":
                                            {
                                                SET.Data = StorageImage.GetImage(fileInfo.FileId);
                                            }
                                            break;
                                    }
                                    
                                }
                            }
                        }
                        break;

                    case p_rt.PUT:
                        {
                            


                        }
                        break;

                    case p_rt.POST:
                        {
                            if (GET.DataFormat.Equals(p_fdt.Json))
                            {
                                TransportCommand TCommand = ConvertJson.buffer_in_string_json<TransportCommand>(GET.Data);

                                if (TCommand.Command.Equals("register"))
                                {
                                    SqlUserType user = ConvertJson.buffer_in_string_json<SqlUserType>(TCommand.ExData);
                                    ResultUserType result = Commands.Command(TCommand.Command, user) as ResultUserType;
                                    SET.Data = Convert.ConvertJson.ConvertJson.Object_in_json_in_buffer<ResultUserType>(result);
                                }
                                else if (TCommand.Command.Equals("login"))
                                {
                                    SqlUserType user = ConvertJson.buffer_in_string_json<SqlUserType>(TCommand.ExData);
                                    ResultUserType result = Commands.Command(TCommand.Command, user) as ResultUserType;
                                    SET.Data = Convert.ConvertJson.ConvertJson.Object_in_json_in_buffer<ResultUserType>(result);
                                }
                            }
                            else if (GET.DataFormat.Equals(p_fdt.File))
                            {
                                FilePatterns fileInfo = ConvertJson.buffer_in_string_json<FilePatterns>(GET.Data);
                                switch (fileInfo.Command)
                                {
                                    case "image_add":
                                        {
                                            bool r = StorageImage.Add(fileInfo.FileName, fileInfo.Image);
                                            if (r)
                                                SET.Data = ConvertJson.Object_in_json_in_buffer("SUCCESS");
                                            else
                                                SET.Data = ConvertJson.Object_in_json_in_buffer("FAILED");
                                        }
                                        break;
                                }
                            }
                        }
                        break;

                    case p_rt.DELETE:
                        {

                        }
                        break;
                }
                return SET;
            }
            else
                return null;
        }
        
    }
}
