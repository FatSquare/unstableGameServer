using System;
using System.Net;
namespace ServerManager
{
    class Program
    {
        // you can  modify these variables if you need to
        public static string serverName = "[US] Main";
        public static int maxplayers = 32;

        // please do not modify these variables if you don't know what you are doing
        public static int port = 6329;
        public static bool ServerIsRunning = true;
        public static bool isDebugging = false; // this will declare whether should debug be 0 or 1 by default
        public static string[] bannedIps;

        static void Main(string[] args)
        {
            Console.Title = serverName;
            Server.StartServer(maxplayers, port);
            while (ServerIsRunning)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("> ");
                string cmd = Console.ReadLine();
                CommandsManager(cmd);
            }
        }
        static void CommandsManager(string _cmd)
        {
            string[] fullCommand = _cmd.Split(" ");
            switch (fullCommand[0].ToLower())
            {
                case "help":
                    Commands.Help(fullCommand);
                    break;
                case "clear":
                    Commands.ClearConsole(fullCommand);
                    break;
                case "ban":
                    Commands.BanUser(fullCommand);
                    break;
                case "debug":
                    Commands.Debug(fullCommand);
                    break;
                case "exit":
                    Commands.StopServer(fullCommand);
                    break;
                default:
                    Console.WriteLine($"Invalid Command: {_cmd}");
                    Console.WriteLine($"type help for more informations");
                    break;
            }
        }
    }
}
