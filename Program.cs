using System;
using System.Reflection;
using System.Reflection.Metadata;
using TCPGameServer.Attributes;
using System.Collections.Generic;
using TCPGameServer.HandlerFolder;


namespace TCPGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GameServer";

            
            HandlerManager handlerManager = new HandlerManager();
            object[] denemeArray = new object[] { "dervis", 5 };
            handlerManager.RunHandler(Opcodes.GetUser,denemeArray);
            

            Server.SetupServer();
            Server.StartServer();
            Console.ReadKey();

        }

        /*
        public static IEnumerable<Type> GetTypesWithMyAttribute()
        {
            var asmbly = Assembly.GetExecutingAssembly();
            foreach (Type type in asmbly.GetTypes())
            {
                if (Attribute.IsDefined(type, typeof(SocketControllerAttribute)))
                    yield return type;
            }
        }

        static List<Type> GetClassesWithAttribute<T>() where T : Attribute
        {
            List<Type> attributedClasses = new List<Type>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetCustomAttribute<T>() != null)
                {
                    attributedClasses.Add(type);
                }
            }

            return attributedClasses;
        }
        */

    }
}
