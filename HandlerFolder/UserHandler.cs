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

        [SocketAction(Opcodes.LoginUser,Authorise.User)]
        public async Task LoginUser()
        {
            //await Console.Out.WriteLineAsync(deneme+" "+ deneme2.ToString());

            await Console.Out.WriteLineAsync("Get user handler called.");
        }
        [SocketAction(Opcodes.RegisterUser, Authorise.User)]
        public async Task RegisterUser()
        {
            //await Console.Out.WriteLineAsync(deneme+" "+ deneme2.ToString());

            await Console.Out.WriteLineAsync("Get user handler called.");
        }

        [SocketAction(Opcodes.SetUser,Authorise.User)]
        public async Task SetHandler()
        {
            await Console.Out.WriteAsync("Set user handler called.");
        }

    }
}
