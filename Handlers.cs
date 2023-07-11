using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TCPGameServer
{
    public class Handlers
    {
        public enum Server
        {
            Hello=1
        }
        public enum ClientEnum
        {
            Hello=1
        }
        public class Packet
        {
            public int id;
            public int type;
        }
        public class Hello: Packet 
        {
            public string message;
            public string name;
        }
        public static Hello Create_Hello(int _id,int _type,string _message)
        {
            Hello packet= new Hello();
            packet.id = _id;
            packet.type = _type;
            packet.message = _message;
            return packet;
        }
        public static void Handle(string jsonData)
        {
            Packet mainPacket = JsonConvert.DeserializeObject<Packet>(jsonData);
            switch (mainPacket.type)
            {
                case (int)ClientEnum.Hello:
                    {
                        Get_Hello(JsonConvert.DeserializeObject<Hello>(jsonData));
                        break;
                    }
                default:
                    break;
            }

        }
        public static void Get_Hello(Hello packet)
        {
            Console.WriteLine($"Player name: {packet.name}");
        }
    }
}
