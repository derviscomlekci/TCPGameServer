using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPGameServer.Attributes;

namespace TCPGameServer.HandlerFolder
{
    public class ProductHandler : BaseHandler
    {

        public ProductHandler()
        {

        }

        [SocketAction(Opcodes.GetProduct,Authorise.User )]
        public async Task Get()
        {

        }
    }
}
