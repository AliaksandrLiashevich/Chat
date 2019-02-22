using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ChatServer
{
    public interface IStorage
    {
        /// <summary>
        /// Concurrent collection that stores messages of all clients in chat
        /// </summary>
        ConcurrentQueue<string> Store { get; set; }

        /// <summary>
        /// Method adds sender name and message to storage
        /// </summary>
        /// <param name="name">Sender name</param>
        /// <param name="message">Sended message</param>
        void AddStoreMessage(string name, string message);

        /// <summary>
        /// Method adds information('name') of clients with lost connection
        /// </summary>
        /// <param name="lostConnection">Collection of clients with lost connection</param>
        void AddLostConnectionMessages(List<ClientInfo> lostConnection);
    }
}
