using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ChatServer
{
    public delegate void Broadcast(string message, ClientInfo clientInfo);

    public class Storage: IStorage
    {
        private readonly int storeMessagesCount;

        public ConcurrentQueue<string> Store { get; set; }

        /// <summary>
        /// Constructor initializes max number of storing messages and concurrent collection for storing
        /// </summary>
        public Storage()
        {
            storeMessagesCount = 10;

            Store = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// Method adds sender name and message to storage
        /// </summary>
        /// <param name="name">Sender name</param>
        /// <param name="message">Sended message</param>
        public void AddStoreMessage(string name, string message)
        {
            if (Store.Count >= storeMessagesCount)
            {
                Store.TryDequeue(out string result);
            }

            Store.Enqueue(name + ":" + message);
        }

        /// <summary>
        /// Method adds information('name') of clients with lost connection
        /// </summary>
        /// <param name="lostConnection">Collection of clients with lost connection</param>
        public void AddLostConnectionMessages(List<ClientInfo> lostConnection)
        {
            foreach (var item in lostConnection)
            {
                AddStoreMessage("Server", item.Name + " is disconnected\n");
            }
        }
    }
}
