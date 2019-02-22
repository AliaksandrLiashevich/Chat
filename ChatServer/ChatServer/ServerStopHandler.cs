using System;
using System.Collections.Generic;

namespace ChatServer
{
    public class ServerStopHandler: IServerStopHandler
    {
        private ISocketHandler socketHandler;

        public ClientInfo Stub { get; set; }

        public Broadcast Broadcast { get; set; }

        /// <summary>
        /// Constructor initializes instance for handling registered sockets
        /// </summary>
        /// <param name="_socketHandler">Instance for manage sockets</param>
        public ServerStopHandler(ISocketHandler _socketHandler)
        {
            socketHandler = _socketHandler;
        }

        /// <summary>
        /// Method handles situation when server stops
        /// </summary>
        /// <param name="clients">Collection of connected clients</param>
        public void StopHandling(List<ClientInfo> clients)
        {
            WaitStop();

            string farewellMessage = "The server has been shut down. Please, try to connect later";

            if(Broadcast != null)
            {
                Broadcast(farewellMessage, Stub);
            }

            socketHandler.CloseConnections(clients);
        }

        /// <summary>
        /// Method waits for 'stop' word in console
        /// </summary>
        private void WaitStop()
        {
            while (true)
            {
                Console.WriteLine("Please, enter 'stop' to shutdown server");

                if (Console.ReadLine() == "stop") return;
            }
        }
    }
}
