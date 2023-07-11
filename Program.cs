using System;

namespace TCPGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GameServer";
            Server.SetupServer();
            Server.StartServer();
            Console.ReadKey();
        }
    }
}
