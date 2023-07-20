using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TCPGameServer.Attributes;
using TCPGameServer.Dto;
using TCPGameServer.Services;
using static TCPGameServer.Handlers;

namespace TCPGameServer.HandlerFolder
{
    public class UserHandler : BaseHandler
    {
        IClient _client;
        public UserHandler(IClient client)
        {
            _client = client;
        }


        [SocketAction(Opcodes.LoginUser,Authorise.User)]
        public async Task LoginUser()
        {

            await Console.Out.WriteLineAsync("Get user handler called.");
        }
        [SocketAction(Opcodes.RegisterUser, Authorise.User)]
        public async Task RegisterUser(string jsonData)
        {
            RegisterPacket data = JsonConvert.DeserializeObject<RegisterPacket>(jsonData);
            UserDetails tempUser= new  UserDetails(data.username, data.password, data.tckno, data.address);
            _client.SetUserDetails(tempUser);
            
            await Console.Out.WriteLineAsync("Get user handler called.");
        }

        [SocketAction(Opcodes.SetUser,Authorise.User)]
        public async Task SetHandler()
        {
            await Console.Out.WriteAsync("Set user handler called.");
        }

    }
}
