using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPGameServer.Dto;

namespace TCPGameServer.Services
{
    public interface IClient
    {
        public void SendDataFromJson(string jsonData);
        public void SetUserDetails(UserDetails userDetails);
    }
}
