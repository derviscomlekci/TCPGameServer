using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TCPGameServer.Dto;
using TCPGameServer.HandlerFolder;

namespace TCPGameServer.Services
{
    public class Client:IClient
    {

        public TcpClient socket;
        public NetworkStream stream;
        public byte[] buffer;//veri alışverişleri için
        //public readonly int id;
        public int roomId = -1;
        string receivedData;

        //Variables
        public bool isSearchGame = false;

        UserDetails userDetails=new UserDetails();

        private readonly IHandler _handler;

        public  Client(IHandler handler)
        {
            _handler = handler;
        }
      

        public void Disconnect()
        {
            if (socket != null && socket.Connected)//***********
                socket.Close();

            if (stream != null)
                stream.Close();
            socket = null;
            stream = null;
            Console.WriteLine($"{userDetails.username}' isimli oyuncu çıktı.");
            buffer = null;
            isSearchGame = false;
            if (roomId != -1)
            {
                //Server.roomsDic[roomId].PlayerDisconnectRoom(id);
            }
            //Name = null;
        }
        public void Connect(TcpClient _socket)
        {
            socket = _socket;
            socket.ReceiveBufferSize = Server.dataBufferSize;//Oyuncudan gelecek buffer büyüklüğü.
            socket.SendBufferSize = Server.dataBufferSize;//oyuncuya gönderilecek buffer büyüklüğü.

            stream = socket.GetStream();
            buffer = new byte[Server.dataBufferSize];// ilk başta vermemizin sebebi kullanıcı yokken boşa buffer tutmamak.

            stream.BeginRead(buffer, 0, Server.dataBufferSize, ReceiveCallback, null);

        }
        //Burada kullanıcıdan veri gelirse veriyi kontrol edip işleyeceğiz.
        public async void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                int receivedDataSize = stream.EndRead(asyncResult);
                if (receivedDataSize <= 0) //Bağlantıya ne olmuş diye  bakacağız. Burada patlamış 0 dan küçük
                {
                    Disconnect();
                    return;
                }
                //Patlamamışsa
                byte[] data = new byte[receivedDataSize];
                Array.Copy(buffer, data, receivedDataSize);//dataya bufferden gelenleri kopyalıyoruz.

                //gelen veriyi jsona çevirip handle için yolluyoruz.
                string receivedJsonData = Encoding.UTF8.GetString(data);
                // Handlers.Handle(receivedJsonData);
                
                receivedData=await _handler.PacketReceived(receivedJsonData);
                SendDataFromJson(receivedJsonData);
                stream.BeginRead(buffer, 0, Server.dataBufferSize, ReceiveCallback, null);


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
            byte[] _data = Encoding.UTF8.GetBytes(jsonData);
            try
            {
                stream.BeginWrite(_data, 0, _data.Length, SendCallback, null);
            }
            catch (Exception)
            {
                Disconnect();
                return;
            }
        }
        public void SendCallback(IAsyncResult asyncResult)
        {
            stream.EndRead(asyncResult);
        }

        public void SetUserDetails(UserDetails userDetails)
        {
            this.userDetails = userDetails;
        }
    }
}
