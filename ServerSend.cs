using System;

namespace ServerManager
{
    public class ServerSend
    {
        //Welcome function will be called each time a player is connected
        public static void Welcome(int playerId, string _msg)
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {
                packet.Write(_msg);
                packet.Write(playerId);

                SendTCPDataToPlayer(playerId, packet);
            }
        }

        private static void SendTCPDataToPlayer(int playerId, Packet packet)
        {
            packet.WriteLength();
            Server.clients[playerId].tcp.SendData(packet);
        }

        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int playerId = 1; playerId < Server.MaxPlayers; playerId++)
            {
                Server.clients[playerId].tcp.SendData(packet);
            }
        }

        private static void SendTCPDataToAll(Packet packet, int exceptPlayerId)
        {
            packet.WriteLength();
            for (int playerId = 1; playerId < Server.MaxPlayers; playerId++)
            {
                if (playerId != exceptPlayerId)
                {
                    Server.clients[playerId].tcp.SendData(packet);
                }
            }
        }
    }
}