using System;
using System.Reflection;
using System.Reflection.Metadata;
using TCPGameServer.Attributes;
using System.Collections.Generic;
using TCPGameServer.HandlerFolder;
using Microsoft.Extensions.DependencyInjection;
using TCPGameServer.Services;
using TCPGameServer.Dto;
using MessagePack;

namespace TCPGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GameServer";

            //string jsonFile=MessagePackSerializer.ConvertToJson(bytes);
            //string directJson = MessagePackSerializer.SerializeToJson(packet);



            var serviceProvider = new ServiceCollection()
            .AddSingleton<IHandler, HandlerManager>()
            .AddSingleton<IClient, Client>()
            .BuildServiceProvider();

           // HandlerManager manager = new HandlerManager();
            //HandlerManager handlerManager = new HandlerManager();
            //object[] denemeArray = new object[] { "dervis", 5 };
            //handlerManager.RunHandler(Opcodes.SetUser,null);
            

            Server.SetupServer();
            Server.StartServer();
            Console.ReadKey();

        }

    }
}
