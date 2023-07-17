using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPGameServer.Attributes;

namespace TCPGameServer.HandlerFolder
{
    public class UserHandler : BaseHandler
    {
        public UserHandler()
        {

        }

        [SocketAction(Opcodes.GetUser,"user")]
        public async Task Get()
        {

        }
    }
}
