using System;
using TCPGameServer.Attributes;
using TCPGameServer.HandlerFolder;

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
            var attribute =(SocketActionAttribute)Attribute.GetCustomAttribute(typeof(ProductHandler), typeof(SocketActionAttribute));
        }
    }
}
