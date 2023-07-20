using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TCPGameServer.Services;

namespace TCPGameServer
{
    public class Room
    {
        //Players
        public int roomId;
        public bool isRoomFull;
        public bool isStarted = false;
        public Player player1;
        public Player player2;


        public Room(int id)
        {
            roomId = id;
            player1 = new Player();
            player2 = new Player();
        }
        public void StartRoom(int _player1Id,int _player2Id)
        {
            player1.id = _player1Id;
            player2.id = _player2Id;
            Server.clientsDic[player1.id].roomId = roomId;
            Server.clientsDic[player2.id].roomId = roomId;
            Server.clientsDic[player1.id].isSearchGame = false;
            Server.clientsDic[player2.id].isSearchGame = false;
            isRoomFull = true;
            
        }
        public void ClearRoom()
        {
            isRoomFull = false;
            isStarted = false;
        }
        public void PlayerConnectRoom(int _id)
        {
            if (player1.id==_id)
            {
                player1.isInRoom = true;
                if (player2.isInRoom)
                {
                    //Server.clientsDic[player2.id].tcp.SendDataFromJson()
                }

            }
            else if (player2.id==_id)
            {
                player2.isInRoom = true;
                if (player1.isInRoom)
                {
                    //Server.clientsDic[player2.id].tcp.SendDataFromJson()
                }
            }
            else
            {
                return;
            }
           // if(player1.isInRoom && player2.isInRoom)
              //  Console.WriteLine($"Room number:{roomId}, Oyuncular: {Server.clientsDic[player1.id].tcp.Name}, {Server.clientsDic[player2.id].tcp.Name}");
        }
        public void PlayerDisconnectRoom(int _id)
        {
            if (player1.id == _id)
            {
                player1.isInRoom = false;
               // Console.WriteLine($"{Server.clientsDic[_id].tcp.Name}' isimli oyuncu {roomId}' numaralı odadan çıktı.");
            }
            else if (player2.id == _id)
            {
                player2.isInRoom = false;
               // Console.WriteLine($"{Server.clientsDic[_id].tcp.Name}' isimli oyuncu {roomId}' numaralı odadan çıktı.");
            }
            else
            {
                return;
            }
            if (!player1.isInRoom && !player2.isInRoom)
            {
                ClearRoom();
                Console.WriteLine($"Tum oyuncular çıktı. Oda numarası:{roomId}");
            }
        }
        public void SendChatMessage(string _message,int senderId)
        {
            //Server.clientsDic[player1.id].tcp.SendDataFromJson(JsonConvert.SerializeObject(Handlers.ChreateChatMessage(player1.id, (int)(Handlers.ServerEnum.ChatMessage), _message, senderId)));
           // Server.clientsDic[player2.id].tcp.SendDataFromJson(JsonConvert.SerializeObject(Handlers.ChreateChatMessage(player2.id, (int)(Handlers.ServerEnum.ChatMessage), _message, senderId)));
        }

        public class Player
        {
            public int id=0;
            public bool isInRoom = false;
        }
    }
}
