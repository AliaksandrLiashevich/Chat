using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Server
    {
        private readonly int bufferSize;
        private int currentId;
        private object locker = new object();

        private Socket listener;
        private ClientInfo stub;

        private IStorage storage;
        private ISocketHandler socketHandler;
        private IStoreMessageSender storeMessageSender;
        private ILostConnectionSender lostConnectionSender;
        private IServerStopHandler serverStopHandler;

        private List<ClientInfo> clients = new List<ClientInfo>();
        private List<ClientInfo> lostClients = new List<ClientInfo>();

        /// <summary>
        /// Constructor initializes all internal instances, connect all external dependencies
        /// </summary>
        /// <param name="_socketHandler">Instance of SocketHabdker type</param>
        /// <param name="_storage"></param>
        /// <param name="_storeMessageSender"></param>
        /// <param name="_lostConnectionSender"></param>
        /// <param name="_serverStopHandler"></param>
        public Server(ISocketHandler _socketHandler, IStorage _storage, IStoreMessageSender _storeMessageSender,
            ILostConnectionSender _lostConnectionSender, IServerStopHandler _serverStopHandler)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);
            listener.Listen(10);

            storage = _storage;
            socketHandler = _socketHandler;
            storeMessageSender = _storeMessageSender;
            lostConnectionSender = _lostConnectionSender;
            serverStopHandler = _serverStopHandler;

            serverStopHandler.Broadcast = BroadcastMessage;
            lostConnectionSender.Broadcast = BroadcastMessage;

            stub = new ClientInfo(currentId++, null, null);

            serverStopHandler.Stub = stub;
            lostConnectionSender.Stub = stub;

            bufferSize = listener.ReceiveBufferSize;
        }

        /// <summary>
        /// Method starts all other handler methods in server
        /// </summary>
        public void StartWorking()
        {
            Console.WriteLine("Server start working");

            StartListening();

            serverStopHandler.StopHandling(clients);
        }

        /// <summary>
        /// Method broadcasts message to all clients except sender
        /// </summary>
        /// <param name="message">Text of message</param>
        /// <param name="clientInfo">Instance with all information about client</param>
        private void BroadcastMessage(string message, ClientInfo clientInfo)
        {
            foreach (var client in clients)
            {
                try
                {
                    if (client.Id != clientInfo.Id)
                    {
                        client.Socket.Send(Encoding.UTF8.GetBytes(message));
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Method starts communication between client and server
        /// </summary>
        /// <param name="clientInfo">Instance with all information about client</param>
        /// <returns>Running Task</returns>
        private Task CommunicateProcess(ClientInfo clientInfo)
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    string message = ReceiveMessage(clientInfo.Socket) + "\n";

                    storage.AddStoreMessage(clientInfo.Name, message);

                    lock (locker)
                    {
                        List<ClientInfo> list = socketHandler.HandleConnections(clients);

                        storage.AddLostConnectionMessages(list);

                        lostConnectionSender.BroadcastLostConnections(list);
                    }

                    BroadcastMessage(clientInfo.Name + ":" + message, clientInfo);

                    Console.Write(clientInfo.Name + ":" + message);
                }
            });
        }

        /// <summary>
        /// Method sends name request to client
        /// </summary>
        /// <param name="socket">Instance of definite connection between server and client</param>
        private void SendNameRequest(Socket socket)
        {
            string message = "Please, enter your name\n";

            socket.Send(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Method execute basic actions like create new server-client connection and run handler methods
        /// </summary>
        /// <returns>Running Task</returns>
        private Task StartListening()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    Socket socket = listener.Accept();

                    SendNameRequest(socket);

                    ClientInfo clientInfo = new ClientInfo(currentId++, ReceiveMessage(socket), socket);

                    clients.Add(clientInfo);

                    storeMessageSender.SendStoreMessages(socket);

                    CommunicateProcess(clientInfo);
                }
            });
        }

        /// <summary>
        /// Method receives message from specify socket
        /// </summary>
        /// <param name="socket">Instance of definite connection between server and client</param>
        /// <returns>Message text</returns>
        private string ReceiveMessage(Socket socket)
        {
            string message = null;

            while (true)
            {
                Thread.Sleep(200);

                if (socket.Available > 0)
                {
                    byte[] receivedBytes = new byte[bufferSize];

                    int bytesCount = socket.Receive(receivedBytes);

                    message = Encoding.UTF8.GetString(receivedBytes, 0, bytesCount);

                    break;
                }
            }

            return message;
        }
    }
}
