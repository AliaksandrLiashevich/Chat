using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class StoreMessagesSender: IStoreMessageSender
    {
        private IStorage storage;

        /// <summary>
        /// Constructor initializes storage for store messages
        /// </summary>
        /// <param name="_storage">Instance of srorage interface type</param>
        public StoreMessagesSender(IStorage _storage)
        {
            storage = _storage;
        }

        /// <summary>
        /// Method for sending last messages to recetly connected client
        /// </summary>
        /// <param name="socket">Instance of object, that connects client and server</param>
        public void SendStoreMessages(Socket socket)
        {
            foreach (var message in storage.Store)
            {
                socket.Send(Encoding.UTF8.GetBytes(message));
            }
        }
    }
}
