using System.Collections.Generic;

namespace ChatServer
{
    public interface ISocketHandler
    {
        /// <summary>
        /// Method controls the process of checking the status of sockets and their removal
        /// </summary>
        /// <param name="clientsConnected">Collection of connected clients</param>
        /// <returns>Collection of recently removed clients</returns>
        List<ClientInfo> HandleConnections(List<ClientInfo> clientsConnected);

        /// <summary>
        /// Method closes connection of all registered clients
        /// </summary>
        /// <param name="list">Collection of connected clients</param>
        void CloseConnections(List<ClientInfo> list);
    }
}
