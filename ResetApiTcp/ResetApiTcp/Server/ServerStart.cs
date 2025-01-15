using ResetApiTcp.Cliernt;
using System.Net;
using System.Net.Sockets;

namespace ResetApiTcp.Server
{
    internal class ServerStart
    {
        private TcpListener listener;
        private bool ExitMainThread = false;
        public bool GetExitMainThread() 
        {
            return ExitMainThread;
        }
        public void SetGetExitMainThread(bool MainThreadOff) 
        {
            ExitMainThread = MainThreadOff;
        }
        private void Init(int startPort) 
        {
            GeneralMeaning.GeneralMeaning.imgs_defult = new Dictionary<string, string>();
            GeneralMeaning.GeneralMeaning.imgs_defult.Add("user_defult_img", "./Resource/Img/user_defult_img.png");
            listener = new TcpListener(IPAddress.Any, startPort);
        }
        public ServerStart(int startPort) 
        {
            
            Init(startPort);
        }
        public void Start() 
        {
            listener.Start();
        }
        public async void Loop() 
        {
            TcpClient client = null;
            try
            {
               client = listener.AcceptTcpClient();
            }
            catch (Exception ex) 
            {

            }
            await Task.Run(() =>
            {
                if (client != null) {
                    new ClientThread(client);
                }
                //... thread sleep(500);
            });
        }
        public void Stop() 
        {
            listener.Stop();
        }
    }
}
