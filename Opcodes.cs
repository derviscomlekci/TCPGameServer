using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer
{
    public enum Opcodes
    {
        Login = 1,
        SearchGame = 2,
        ConnectRoom = 3,
        ChatMessage = 4,
        GetProduct = 5,
        GetUser = 6,
        SetUser=7
    }
}
