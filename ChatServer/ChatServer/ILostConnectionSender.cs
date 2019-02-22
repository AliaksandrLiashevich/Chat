using System.Collections.Generic;

namespace ChatServer
{
    public interface ILostConnectionSender
    {
        /// <summary>
        /// Stub for broadcasting data to all clients
        /// </summary>
        ClientInfo Stub { get; set; }

        /// <summary>
        /// Instance of delegate type for broadcasting data used 'Server' method
        /// </summary>
        Broadcast Broadcast { get; set; }

        /// <summary>
        /// Method broadcasts names of clients with lost connected
        /// </summary>
        /// <param name="list">Collection of connected clients</param>
        void BroadcastLostConnections(List<ClientInfo> list);
    }
}
