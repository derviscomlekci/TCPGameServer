using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    public class LoginPacket:Packet
    {
        public string username;
        public string password;
    }
}
