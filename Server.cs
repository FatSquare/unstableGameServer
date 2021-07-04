using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ServerManager
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static TcpListener tcpListener;
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        public static void StartServer(int _maxPlayers, int _port)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Starting Server..");

            MaxPlayers = _maxPlayers;
            Port = _port;
            IntializeServerData();
            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Server started on port {Port}!");

        }

        static void TCPConnectCallBack(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);

            if (Program.isDebugging)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{_client.Client.RemoteEndPoint} is trying to connect!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("> ");
            }

            //check if there is a place left in the server
            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    //connect the user;
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            if (Program.isDebugging)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server is full!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("> ");
            }
        }
        static void IntializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
