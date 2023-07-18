using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TCPGameServer.Attributes;

namespace TCPGameServer.HandlerFolder
{
    public class UserHandler : BaseHandler
    {
        public UserHandler()
        {

        }

        [SocketAction(Opcodes.GetUser,Authorise.User)]
        public async Task GetHandler()
        {
            await Console.Out.WriteLineAsync("Get user handler called.");
        }

        [SocketAction(Opcodes.SetUser,Authorise.User)]
        public async Task SetHandler()
        {
            await Console.Out.WriteAsync("Set user handler called.");
        }
    }
}
