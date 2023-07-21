using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.HandlerFolder
{
    public interface IHandler
    {
        Task<string> RunHandler(Opcodes opcode, object[] objectArray);
        Task<string> PacketReceived(string jsonData);
    }
}
