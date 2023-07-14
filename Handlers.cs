﻿using System;
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
            SearchGame = 2,
            ConnectRoom = 3,
            ChatMessage = 4
        }
        public enum ClientEnum
        {
            Hello = 1,
            SearchGame = 2,
            ConnectRoom = 3,
            ChatMessage = 4

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
                case (int)ClientEnum.ConnectRoom:
                    {
                        Get_ConnectRoom(JsonConvert.DeserializeObject<ConnectRoom>(jsonData)); break;
                    }
                case (int)ClientEnum.ChatMessage:
                    {
                        Get_ChatMessage(JsonConvert.DeserializeObject<ChatMessage>(jsonData)); break;
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

            if (packet.search)//Arama yapılıyorsa başka arayan bir kullanıcı bulucaz.
            {
                Console.WriteLine($"{Server.clientsDic[packet.id].tcp.Name}'den arama isteği geldi. Aranıyor");
                for (int i = 1; i <= Server.MaxPlayers; i++)
                {
                    if (Server.clientsDic[i].tcp.isSearchGame && packet.id != i)
                    {

                        for (int x = 1; x <= Server.roomsDic.Count; x++)
                        {
                            if (!Server.roomsDic[x].isRoomFull)
                            {
                                //Boş bir oda varsa
                                Server.roomsDic[x].StartRoom(Server.clientsDic[i].tcp.id, Server.clientsDic[packet.id].tcp.id);
                                //Eşleştirme yazılacak.
                                Server.clientsDic[i].tcp.SendDataFromJson(JsonConvert.SerializeObject(CreateSearch(i, (int)ServerEnum.SearchGame, false, true)));
                                Server.clientsDic[packet.id].tcp.SendDataFromJson(JsonConvert.SerializeObject(CreateSearch(packet.id, (int)ServerEnum.SearchGame, false, true)));

                                // Console.WriteLine(Server.clientsDic[i].tcp.Name + " ve " + Server.clientsDic[packet.id].tcp.Name + " arasında bağlantı kuruldu.Oda numarası :"+x);
                                return;

                            }
                        }

                    }
                }

            }
            else
            {
                Console.WriteLine($"{Server.clientsDic[packet.id].tcp.Name}'den gelen arama isteği iptal edildi.");
            }
        }
        public class ConnectRoom : Packet
        {
            public bool isConnect;
        }

        public static ConnectRoom CreateConnectRoom(int _id, int _type, bool _connect)
        {
            ConnectRoom packet = new ConnectRoom();
            packet.id = _id;
            packet.type = _type;
            packet.isConnect = _connect;
            return packet;
        }
        public static void Get_ConnectRoom(ConnectRoom packet)
        {
            if (!packet.isConnect)
            {
                Server.roomsDic[Server.clientsDic[packet.id].tcp.roomId].PlayerDisconnectRoom(packet.id);//oyuncu odadan çıktıysa
                return;
            }

            Server.roomsDic[Server.clientsDic[packet.id].tcp.roomId].PlayerConnectRoom(packet.id);//oyuncu sahneyi yükledi ve hazır
            return;

        }
        public class ChatMessage : Packet
        {
            public string message;
        }
        public static ChatMessage ChreateChatMessage(int _id, int _type, string _message)
        {
            ChatMessage packet = new ChatMessage();
            packet.id = _id;
            packet.type = _type;
            packet.message = _message;
            return packet;

        }
        public static void Get_ChatMessage(ChatMessage packet)
        {
            Server.roomsDic[Server.clientsDic[packet.id].tcp.roomId].SendChatMessage($"{Server.clientsDic[packet.id].tcp.Name}: "+ packet.message);
        }

    }
}


