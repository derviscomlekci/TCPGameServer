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
        /*
        public UserHandler(IClient client)
        {
            _client = client;
        }
        */


        [SocketAction(Opcodes.LoginUser,Authorise.User)]
        public async Task LoginUser(string jsonData)
        {
            LoginPacket packet=JsonConvert.DeserializeObject<LoginPacket>(jsonData);
            //TODO
            //Eğer böyle bir kullanıcı varsa.
            _client.SendDataFromJson(JsonConvert.SerializeObject(new LoginResponse((int)Opcodes.LoginUserResponse,true, "Login Successfully")));
            await Console.Out.WriteLineAsync($"{packet.username} is loggined in server.");
        }



        [SocketAction(Opcodes.RegisterUser, Authorise.User)]
        public async Task<string> RegisterUser(string jsonData)
        {
            RegisterPacket data = JsonConvert.DeserializeObject<RegisterPacket>(jsonData);
            UserDetails tempUser= new  UserDetails(data.username, data.password, data.tckno, data.address);

            //Check is user already registered.
            //_client.SetUserDetails(tempUser);
            //_client.SendDataFromJson(JsonConvert.SerializeObject(new RegisterResponse((int)Opcodes.RegisterUserResponse, true, "Register succesfully.")));

            //clientt.SetUserDetails(tempUser);
            //clientt.SendDataFromJson(JsonConvert.SerializeObject(new RegisterResponse((int)Opcodes.RegisterUserResponse, true, "Register succesfully.")));
            string json= JsonConvert.SerializeObject(new RegisterResponse((int)Opcodes.RegisterUserResponse, true, "Register succesfully."));
            return json;
            await Console.Out.WriteLineAsync($"{data.username}' is registered.");
        }



        [SocketAction(Opcodes.SetUser,Authorise.User)]
        public async Task SetHandler()
        {
            await Console.Out.WriteAsync("Set user handler called.");
        }



    }
}
