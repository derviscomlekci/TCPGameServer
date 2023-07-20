using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    public class LoginResponse
    {
        public bool IsLoggedIn { get; set; }
        public string ErrorMessage;
    }
}
