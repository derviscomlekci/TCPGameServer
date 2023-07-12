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
        public enum ServerEnum
        {
            Hello = 1,
            SearchGame = 2
        }
        public enum ClientEnum
        {
            Hello = 1,
            SearchGame = 2
        }

        public static void Handle(string jsonData)
        {
            Packet mainPacket = JsonConvert.DeserializeObject<Packet>(jsonData);
            switch (mainPacket.type)
            {
                case (int)ClientEnum.Hello:
                    {
                        Get_Hello(JsonConvert.DeserializeObject<Hello>(jsonData)); break;
                    }
                case (int)ClientEnum.SearchGame:
                    {
                        GetSearch(JsonConvert.DeserializeObject<SearchPacket>(jsonData)); break;
                    }

                default:
                    break;
            }

        }
        public class Packet
        {
            public int id;
            public int type;
        }

        public class Hello : Packet
        {
            public string message;
            public string name;
        }
        public static Hello Create_Hello(int _id, int _type, string _message)
        {
            Hello packet = new Hello();
            packet.id = _id;
            packet.type = _type;
            packet.message = _message;
            return packet;
        }

        public static void Get_Hello(Hello packet)
        {
            Server.clientsDic[packet.id].tcp.Name = packet.name;
            Console.WriteLine($"Player name: {packet.name}");
        }
        public class SearchPacket : Packet
        {
            public bool search;//Bulunduysa false
            public bool found;//Bulunduysa true
                              //Bulunduysa search:true, found:true, aranıyorsa search:true, found:false, bulunamadı search:false, found:true

        }

        public static SearchPacket CreateSearch(int _id, int _type, bool _search, bool _found)
        {
            SearchPacket searchPacket = new SearchPacket();
            searchPacket.id = _id;
            searchPacket.type = _type;
            searchPacket.search = _search;
            searchPacket.found = _found;
            return searchPacket;
        }
        public static void GetSearch(SearchPacket packet)
        {
            Server.clientsDic[packet.id].tcp.isSearchGame = packet.search;
            Server.clientsDic[packet.id].tcp.SendDataFromJson(JsonConvert.SerializeObject(CreateSearch(packet.id, (int)ServerEnum.SearchGame, packet.search, false)));
            if (packet.search)
            {
                Console.WriteLine($"{Server.clientsDic[packet.id].tcp.Name}'den arama isteği geldi. Aranıyor");

            }
            else
            {
                Console.WriteLine($"{Server.clientsDic[packet.id]}'den gelen arama isteği iptal edildi.");
            }
        }
    }
}


