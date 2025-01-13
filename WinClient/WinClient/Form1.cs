using Newtonsoft.Json;
using System.Drawing;
using WinClient.WorkingApi;
using WinClient.WorkingApi.Convert;
using WinClient.WorkingApi.Convert.DataTransportationFormat;
using WinClient.WorkingApi.Patterns.File;
using WinClient.WorkingApi.Patterns.TransportCommand;
using WinClient.WorkingApi.Patterns.User;

namespace WinClient
{
    public partial class Form1 : Form
    {
        private string name1;
        private Client client;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = true;
            client = new Client(OutValues.host, OutValues.port);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                client.Connect();
                DataTransportationFormat Set = new DataTransportationFormat();
                DataTransportationFormat Get = new DataTransportationFormat();
                Set.Request = WorkingApi.Patterns.RequestType.GET;
                Set.DataFormat = WorkingApi.Patterns.FormatDataType.File;
                Set.NameUser = Environment.UserName;
                FilePatterns fileInfo = new FilePatterns();
                fileInfo.FileType = FileType.Image;
                int id = Convert.ToInt16(numericUpDown1.Value);
                fileInfo.FileName = $"Name{id}";

                if (id > 0)
                    fileInfo.FileId = id;
                else
                    fileInfo.FileId = 1;
                fileInfo.Command = "get_image";
                Set.Data = ConvertJson.Object_in_json_in_buffer<FilePatterns>(fileInfo);
                client.Write(ConvertJson.Object_in_json_in_buffer<DataTransportationFormat>(Set));
                byte[] image = null;
                byte[] get = null;
                client.Read(ref get);
                Get = ConvertJson.buffer_in_string_json<DataTransportationFormat>(get);
                image = Get.Data;

                new Thread(() =>
                {
                    int size = Get.Data.Length;
                    MessageBox.Show(size.ToString());

                }).Start();
                Image img = ConvertImage.byteArrayToImage(image);

                //img = ConvertImage.byteArrayToImage(File.ReadAllBytes("C:\\Users\\pargev\\Pictures\\devushka_ozero_led_1067884_3840x2400.jpg"));

                pictureBox1.Image = img;
                client.Close();
            }).Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                client.Connect();
                DataTransportationFormat Set = new DataTransportationFormat();
                DataTransportationFormat Get = new DataTransportationFormat();
                Set.Request = WorkingApi.Patterns.RequestType.POST;
                Set.DataFormat = WorkingApi.Patterns.FormatDataType.Json;
                Set.NameUser = Environment.UserName;
                TransportCommand TCommand = new TransportCommand();
                TCommand.Command = "register";
                SqlUserType user = new SqlUserType();
                user.FirstName = "Admin2";
                user.LastName = "Admins";
                user.PhoneNumber = 8_994_138_12_22;
                user.Password = "PasswordAdmin";
                TCommand.ExData = ConvertJson.Object_in_json_in_buffer<SqlUserType>(user);
                Set.Data = ConvertJson.Object_in_json_in_buffer<TransportCommand>(TCommand);
                client.Write(ConvertJson.Object_in_json_in_buffer<DataTransportationFormat>(Set));
                byte[] get = null;
                client.Read(ref get);
                Get = ConvertJson.buffer_in_string_json<DataTransportationFormat>(get);
                ResultUserType resultUser = ConvertJson.buffer_in_string_json<ResultUserType>(Get.Data);
                MessageBox.Show(resultUser.Satus);

                client.Close();
            }).Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                client.Connect();
                DataTransportationFormat Set = new DataTransportationFormat();
                DataTransportationFormat Get = new DataTransportationFormat();
                Set.Request = WorkingApi.Patterns.RequestType.GET;
                Set.DataFormat = WorkingApi.Patterns.FormatDataType.File;
                Set.NameUser = Environment.UserName;
                FilePatterns fileInfo = new FilePatterns();
                fileInfo.FileType = FileType.Image;
                int id = Convert.ToInt16(numericUpDown1.Value);
                fileInfo.FileName = $"Name{id}";
                fileInfo.FileId = id;
                fileInfo.Command = "add_image";
                fileInfo.Image = File.ReadAllBytes(name1);
                Set.Data = ConvertJson.Object_in_json_in_buffer<FilePatterns>(fileInfo);
                client.Write(ConvertJson.Object_in_json_in_buffer<DataTransportationFormat>(Set));

                byte[] get = null;
                client.Read(ref get);
                Get = ConvertJson.buffer_in_string_json<DataTransportationFormat>(get);


                new Thread(() =>
                {
                    int size = Get.Data.Length;
                    MessageBox.Show(size.ToString());

                }).Start();

                //img = ConvertImage.byteArrayToImage(File.ReadAllBytes("C:\\Users\\pargev\\Pictures\\devushka_ozero_led_1067884_3840x2400.jpg"));
                client.Close();
            }).Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            string name = openFileDialog1.FileName;
            name1 = name;
        }
    }
}
