using System.Collections.Generic;

namespace ChatServer
{
    public interface IServerStopHandler
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
        /// Method handles situation when server stops
        /// </summary>
        /// <param name="clients">Collection of connected clients</param>
        void StopHandling(List<ClientInfo> clients);
    }
}
