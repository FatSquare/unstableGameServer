using System;
using System.Net;
namespace ServerManager
{
    class Program
    {
        public static int port = 6329;
        public static string serverName = "[US] Main";
        public static int maxplayers = 32;

        static void Main(string[] args)
        {
            Console.Title = serverName;
            Server.StartServer(maxplayers, port);

            Console.Read();
        }
    }
}
