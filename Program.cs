using System;

namespace TCPGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GameServer";
            Server.Start(50, 26950);
        }
    }
}
