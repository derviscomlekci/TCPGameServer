using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCPGameServer
{
    public class Server
    {
        public static int MaxPlayers { get;private set; }
        public static int Port { get; private set; }

        public static Dictionary<int,Client> Clients =new Dictionary<int, Client>();

        
        private static TcpListener tcpListener;

        public static void Start(int  _maxPlayer,int _port)
        {
            MaxPlayers = _maxPlayer;
            Port = _port;
            Console.WriteLine("Starting server...");
            InitializeServerData();


            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server started on {Port}.");
        }
        private  static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");
            for (int i = 1; i<= MaxPlayers; i++)
            {
                if (Clients[i].tcp.socket==null)
                {
                    Clients[i].tcp.Connect(_client);
                    return;
                }
            }
            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect:Server full!");


        }
        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new Client(i));
            }
        }
    }
}
