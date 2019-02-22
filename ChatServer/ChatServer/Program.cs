namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketHandler socketHandler = new SocketHandler();

            Storage storage = new Storage();

            StoreMessagesSender storeMessagesSender = new StoreMessagesSender(storage);

            LostConnetionsSender lostConnetionsSender = new LostConnetionsSender();

            ServerStopHandler serverStopHandler = new ServerStopHandler(socketHandler);

            Server server = new Server(socketHandler, storage, storeMessagesSender, lostConnetionsSender, serverStopHandler);

            server.StartWorking();
        }
    }
}
