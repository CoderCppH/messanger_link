using ResetApiTcp.Server;
namespace ResetApiTcp.ThreadKeyBoard
{
    class ThreadKeyBoard
    {
        ServerStart _SS;
        public ThreadKeyBoard(ref ServerStart SS)
        {
            _SS = SS;
            new Thread(() => {

                while (!_SS.GetExitMainThread()) 
                {
                    Console.WriteLine("Input commands: ");
                    string comm = Console.ReadLine();
                    switch (comm) 
                    {
                        case "close": 
                            {
                                CloseServer();
                            }break;
                        case "stop": 
                            {
                                CloseServer();
                            } break;
                    }
                }
            
            }).Start();
        }
        private void CloseServer()
        {
            _SS.SetGetExitMainThread(true);
            _SS.Stop();
        }
    }
}
