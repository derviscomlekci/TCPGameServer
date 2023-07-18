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

        [SocketAction(Opcodes.GetProduct,Authorise.Admin )]
        public async Task GetHandler()
        {
            await Console.Out.WriteLineAsync("Get product handler called.");
        }

    }
}
