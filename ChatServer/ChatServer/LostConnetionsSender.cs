using System.Collections.Generic;

namespace ChatServer
{
    public class LostConnetionsSender: ILostConnectionSender
    {
        public Broadcast Broadcast { get; set; }

        public ClientInfo Stub { get; set; }

        public LostConnetionsSender() { }

        /// <summary>
        /// Method broadcasts names of clients with lost connected
        /// </summary>
        /// <param name="list">Collection of connected clients</param>
        public void BroadcastLostConnections(List<ClientInfo> list)
        {
            if (Broadcast != null)
            {
                foreach (var item in list)
                {
                    Broadcast("Server:" + item.Name + " is disconnected\n", Stub);
                }
            }
        }
    }
}
