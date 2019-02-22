using System.Net.Sockets;

namespace ChatServer
{
    public interface IStoreMessageSender
    {
        /// <summary>
        /// Method for sending data to definite client
        /// </summary>
        /// <param name="socket">Specific scoket</param>
        void SendStoreMessages(Socket socket);
    }
}
