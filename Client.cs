using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCPGameServer
{
    public class Client
    {
        private static int dataBufferSize = 4096;//4 mb
        public int id;
        public TCP tcp;

        public Client(int _clientId)
        {
            id = _clientId;
            tcp=new TCP(id);
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;

            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(int _id)
            {
                id = _id;
            }
            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize=dataBufferSize; 
                socket.SendBufferSize=dataBufferSize;

                stream=socket.GetStream();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                //TODO: sende welcome packet



            }
            public void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        //TODO: disconnect
                        return;
                    }
                    byte[] data=new byte[_byteLength];
                    Array.Copy(receiveBuffer, data, _byteLength); 
                    //TODO: handle data
                    stream.BeginRead(receiveBuffer,0,dataBufferSize,ReceiveCallback,null);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving TCP data:{ex}");
                    //TODO:disconnect
                    throw;
                }
            }

        }
    }
}
