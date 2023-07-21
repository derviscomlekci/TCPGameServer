using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPGameServer.Dto
{
    [MessagePackObject]
    public class MessagePackPacket
    {
        [Key("name")]
        public string Name { get; set; }

        [Key("number")]
        public int Number { get; set; }

    }
}
