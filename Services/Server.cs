﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TCPGameServer.HandlerFolder;

namespace TCPGameServer.Services
{
    public class Server
    {

        public static TcpListener tcpListener;

        //Burada 20 kişilik bi koltuk sistemi hazırlamış olduk.
        //Katılan oyuncular buraya oturacak.
        public static Dictionary<int, Client> clientsDic = new Dictionary<int, Client>();
        public static List<Client> clientsList = new List<Client>();
        public static Dictionary<int, Room> roomsDic = new Dictionary<int, Room>();
       // public static int MaxPlayers = 20;
        public static int PORT = 26950;
        public static int dataBufferSize = 4096;
        public static HandlerManager _handlerManager = new HandlerManager();

        public static void SetupServer()
        {
            //Sadece buradan gelen istekleri kabul etmesi için
            tcpListener = new TcpListener(IPAddress.Any, PORT);
            //SetClients();
            //SetRooms();
            Console.WriteLine($"Server is ready");
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
            TcpClient socket = tcpListener.EndAcceptTcpClient(asyncResult);

            //bir kullanıcı aldıktan sonra başka bir kullanıcı gelirse diye tekrar yazıyoruz.
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallBack), null);

            Client newClient = new Client(_handlerManager);
            newClient.Connect(socket);
            clientsList.Add(newClient);

            /*
            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clientsDic[i].socket == null)//koltuk boşsa
                {
                    //yerleştirip return edicez.
                    clientsDic[i].Connect(socket);//Oyuncu artık içeride

                    Console.WriteLine("A new player connected");
                    //Oyuncuya hello gönderiyoruz.
                    clientsDic[i].SendDataFromJson(JsonConvert
                        .SerializeObject(Handlers.Create_Hello(clientsDic[i].id, (int)Handlers.ServerEnum.Hello, "Connection succesfull...")));
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
            */


        }
        /*
        public static void SetClients()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clientsDic.Add(i, new Client(i));
            }
        }
        */
        /*
        public static void SetRooms()
        {
            for (int i = 1; i <= MaxPlayers / 2; i++)
            {
                roomsDic.Add(i, new Room(i));
            }
        }
        */

    }
}
