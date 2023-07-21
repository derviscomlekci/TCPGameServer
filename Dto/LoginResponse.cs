using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    public class LoginResponse:Packet
    {
        public bool IsLoggedIn { get; set; }
        public string ErrorMessage;
        public LoginResponse(int _opcode,bool _isLoggin,string _errorMessage)
        {
            opcode = _opcode;
            IsLoggedIn = _isLoggin;
            ErrorMessage = _errorMessage;
        }
    }
}
