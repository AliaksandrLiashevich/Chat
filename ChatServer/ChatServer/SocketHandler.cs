using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer
{
    public class SocketHandler : ISocketHandler
    {
        private List<ClientInfo> lostConnections = new List<ClientInfo>();

        /// <summary>
        /// Method controls the process of checking the status of sockets and their removal
        /// </summary>
        /// <param name="clientsConnected">Collection of connected clients</param>
        /// <returns>Collection of recently removed clients</returns>
        public List<ClientInfo> HandleConnections(List<ClientInfo> clientsConnected)
        {
            lostConnections.Clear();

            CheckConnectionStatus(clientsConnected);
            DeleteLostConnections(clientsConnected);

            return lostConnections;
        }

        /// <summary>
        /// Method closes connection of all registered clients
        /// </summary>
        /// <param name="list">Collection of connected clients</param>
        public void CloseConnections(List<ClientInfo> list)
        {
            foreach (var item in list)
            {
                item.Socket.Shutdown(SocketShutdown.Both);       
                item.Socket.Close();
            }
        }

        /// <summary>
        /// Method determines status of client: connected or not. 
        /// For this purpose it uses interface of class 'Socket'
        /// </summary>
        /// <param name="clientsConnected">Collection of all clients, that connected to server</param>
        private void CheckConnectionStatus(List<ClientInfo> clientsConnected)
        {
            foreach (var client in clientsConnected)
            {
                bool part1 = client.Socket.Poll(1000, SelectMode.SelectRead);                
                bool part2 = (client.Socket.Available == 0);

                if (part1 && part2)
                {
                    lostConnections.Add(client);
                }
            }
        }

        /// <summary>
        /// Method deletes clients with lost connetion from collection that contains all clients
        /// </summary>
        /// <param name="clientsConnected">Collection of all clients, that connected to server</param>
        private void DeleteLostConnections(List<ClientInfo> clientsConnected)
        {
            foreach (var item in lostConnections)
            {
                clientsConnected.Remove(item);
            }
        }
    }
}
