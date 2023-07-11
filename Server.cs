using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TCPGameServer
{
    public class Server
    {
        
        public static TcpListener tcpListener;

        //Burada 20 kişilik bi koltuk sistemi hazırlamış olduk.
        //Katılan oyuncular buraya oturacak.
        public static Dictionary<int,Client> clientsDic = new Dictionary<int,Client>();
        public static int MaxPlayers = 20;
        public static int PORT = 26950;
        public static int dataBufferSize = 4096;

        public static void SetupServer()
        {
            //Sadece buradan gelen istekleri kabul etmesi için
            tcpListener = new TcpListener(IPAddress.Any, PORT);
            SetClients();
            Console.WriteLine($"Server is ready.Max Player {MaxPlayers}");
        }
        public static void StartServer()
        {
            tcpListener.Start();//Start listening received clients
            //Burada içeriye alım yapıyoruz.
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallBack), null);// buradaki null karşıya bi veri göndermememiz.
            Console.WriteLine("Server is listening...");

        }
        public static void AcceptCallBack(IAsyncResult asyncResult)
        {
            TcpClient socket=tcpListener.EndAcceptTcpClient(asyncResult);


            //bir kullanıcı aldıktan sonra başka bir kullanıcı gelirse diye tekrar yazıyoruz.
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallBack), null);

            for (int i=1; i<=MaxPlayers; i++) 
            {
                if (clientsDic[i].tcp.socket==null)//koltuk boşsa
                {
                    //yerleştirip return edicez.
                    clientsDic[i].tcp.Connect(socket);//Oyuncu artık içeride
 
                    Console.WriteLine("A new player connected");
                    //Oyuncuya hello gönderiyoruz.
                    clientsDic[i].tcp.SendDataFromJson(JsonConvert
                        .SerializeObject(Handlers.Create_Hello(clientsDic[i].tcp.id, (int)Handlers.Server.Hello, "Connection succesfull...")));


                    return;
                }
            }
            try
            {
                socket.Close();
                return;
            }
            catch (Exception)
            {

            }
            

  
        }
        public static void SetClients()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clientsDic.Add(i, new Client(i));
            }
        }

    }
}
