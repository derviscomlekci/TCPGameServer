using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer
{
    public enum Opcodes
    {
        RegisterUser=1,
        LoginUser=2,
        LoginUserResponse=4,
        RegisterUserResponse=5,
        GetProduct = 6,
        GetUser = 7,
        SetUser = 8,
        /*
        SearchGame = 2,
        ConnectRoom = 3,
        ChatMessage = 4,
        GetProduct = 5,
        GetUser = 6,
        AddUser=7,
        Login=8,
        SetUser = 9,
        LoginError=10,
        */
    }
}
