using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer
{
    public class Room
    {
        //Players
        public Player player1;
        public Player player2;
        public int roomId;
        public bool isRoomFull;


        public Room(int id)
        {
            roomId = id;
            player1 = new Player();
            player2 = new Player();
        }
        public void StartRoom(int player1Id,int player2Id)
        {
            player1.id = player1Id;
            player2.id = player2Id;
        }
        public void ClearRoom()
        {
            isRoomFull = false;
        }

        public class Player
        {
            public int id=0;
            public bool isInRoom = false;
        }
    }
}
