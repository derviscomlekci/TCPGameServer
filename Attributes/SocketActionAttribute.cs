using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SocketActionAttribute : Attribute
    {
        public Opcodes opcode;
        public Authorise role;

        public SocketActionAttribute(Opcodes opcode, Authorise role)
        {
            this.opcode = opcode;
            this.role = role;
        }
    }
}
