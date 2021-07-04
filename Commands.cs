using System;
using System.IO;
namespace ServerManager
{
    public class Commands
    {
        public static string bannedIpsPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\" + "/list/banned-ips.txt";
        public static void Help(string[] fullCommand)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            DisplayHelpMessage("Command", "Descrption", "Argument(s)");
            Console.WriteLine("");
            DisplayHelpMessage("help", "display this message");
            DisplayHelpMessage("exit", "close the server");
            DisplayHelpMessage("clear", "clear the console");
            DisplayHelpMessage("debug", "enable or disable console debug", "0/1");
            DisplayHelpMessage("ban", "ban an ip address", "ipAddress");
            DisplayHelpMessage("kick", "kick an user from the server", "username");

        }
        static void DisplayHelpMessage(string cmd, string descrption, string args = "")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(cmd + " ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(args + " ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(descrption);
        }

        public static void ClearConsole(string[] fullCommand)
        {
            Console.Clear();
        }
        public static void BanUser(string[] fullCommand)
        {
            string ip = fullCommand[1];
            File.WriteAllText(bannedIpsPath, File.ReadAllText(bannedIpsPath) + $"\n{ip}");
        }
        public static void KickUser(string[] fullCommand)
        {

        }
        public static void Debug(string[] fullCommand)
        {
            try
            {
                if (fullCommand[1].ToLower() == "1" || fullCommand[1].ToLower() == "true")
                {
                    Program.isDebugging = true;
                }
                else if (fullCommand[1].ToLower() == "0" || fullCommand[1].ToLower() == "false")
                {
                    Program.isDebugging = false;
                }
                else
                {
                    Console.WriteLine($"Invalid Argument(s): {fullCommand[1]}");
                }
            }
            catch
            {
                Console.WriteLine($"Argument(s) Cannot be null");
            }
        }
        public static void StopServer(string[] fullCommand)
        {
            Console.WriteLine("Closing the server..");
            Program.ServerIsRunning = false;
        }
    }
}