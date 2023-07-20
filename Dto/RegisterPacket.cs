using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    public class RegisterPacket:Packet
    {
        public string username;
        public string password;
        public string tckno;
        public string address;
    }
}
