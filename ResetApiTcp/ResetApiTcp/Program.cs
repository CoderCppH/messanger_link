using ResetApiTcp.Server;

namespace ResetApiTcp 
{
    class Programm 
    {
        static void Main() 
        {
            Console.WriteLine("Programm start");
            int port = 24242;
            ServerStart SS = new ServerStart(port); 
            Console.WriteLine("Thread KeyBoard Start");
            new ThreadKeyBoard.ThreadKeyBoard(ref SS);
            SS.Start();
            Console.WriteLine("Server start");
            while (!SS.GetExitMainThread()) 
            {
                Console.WriteLine("Loop");
                SS.Loop();
                Thread.Sleep(500);
            }
            SS.Stop();
            Console.WriteLine("Server stop");
        }
    }
}