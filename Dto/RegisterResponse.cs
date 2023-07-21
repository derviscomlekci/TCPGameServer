using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    public class RegisterResponse :Packet
    {
        public bool IsRegistered { get; set; }
        public string Message { get; set; }
        public RegisterResponse(int _opcode,bool _isRegistered,string _message)
        {
            opcode = _opcode;
            IsRegistered = _isRegistered;
            Message = _message;
        }

    }
}
