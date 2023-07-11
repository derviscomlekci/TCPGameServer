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
        public int id;
        public TCP tcp;
        public Client(int _id)
        {
            id = _id;
            tcp=new TCP(_id);
        }

        public class TCP
        {
            public TcpClient socket;
            public NetworkStream stream;
            public byte[] buffer;//veri alışverişleri için
            public readonly int id;

            //
            public string Name;
            //

            public TCP(int id) 
            { 
                this.id = id;
            }
            public void Disconnect()
            {
                if (socket != null && socket.Connected)//***********
                    socket.Close();

                if (stream != null)
                    stream.Close();
                socket = null;
                stream = null;
                Name = null;
            }
            public void Connect(TcpClient _socket)
            {
                socket=_socket;
                socket.ReceiveBufferSize = Server.dataBufferSize;//Oyuncudan gelecek buffer büyüklüğü.
                socket.SendBufferSize=Server.dataBufferSize;//oyuncuya gönderilecek buffer büyüklüğü.

                stream=socket.GetStream();
                buffer=new byte[Server.dataBufferSize];// ilk başta vermemizin sebebi kullanıcı yokken boşa buffer tutmamak.

                stream.BeginRead(buffer, 0, Server.dataBufferSize, ReceiveCallback, null);
            
            }
            //Burada kullanıcıdan veri gelirse veriyi kontrol edip işleyeceğiz.
            public void ReceiveCallback(IAsyncResult asyncResult)
            {
                try
                {
                    int receivedDataSize=stream.EndRead(asyncResult);
                    if (receivedDataSize <= 0) //Bağlantıya ne olmuş diye  bakacağız. Burada patlamış 0 dan küçük
                    {
                        Disconnect();
                        return;
                    }
                    //Patlamamışsa
                    byte[] data=new byte[receivedDataSize];
                    Array.Copy(buffer, data, receivedDataSize);//dataya bufferden gelenleri kopyalıyoruz.
                    
                    //gelen veriyi jsona çevirip handle için yolluyoruz.
                    string receivedJsonData=Encoding.UTF8.GetString(data);
                    Handlers.Handle(receivedJsonData);

                    stream.BeginRead(buffer,0,Server.dataBufferSize, ReceiveCallback, null);

                }
                catch (Exception)
                {
                    //Disconnect;
                    Disconnect();
                    return;
                }
            }
            public void SendDataFromJson(string jsonData)
            {
                byte[] _data=Encoding.UTF8.GetBytes(jsonData);
                try
                {
                    stream.BeginWrite(_data,0,_data.Length,SendCallback,null);
                }
                catch (Exception)
                {
                    Disconnect();
                    return;
                }
            }
            public void SendCallback(IAsyncResult  asyncResult )
            {
                stream.EndRead(asyncResult);
            }
        }
    }
}
