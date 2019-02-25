using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    public class Client
    {
        private readonly int bufferSize;
        private readonly int messagesCount;
        private readonly int delay;

        private Socket sender;        
        private ITextGenerator textGenerator;
        private Random random = new Random();

        /// <summary>
        /// Constructor initializes all internal instances and bind with external dependencies
        /// </summary>
        /// <param name="_textGenerator">Instance of text generator class</param>
        public Client(ITextGenerator _textGenerator)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);

            textGenerator = _textGenerator;

            bufferSize = sender.ReceiveBufferSize;
            messagesCount = 10;
            delay = 5000;
        }

        /// <summary>
        /// Method starts all handler methods in the current class
        /// </summary>
        public void StartWorking()
        {         
            ReceiveMessages();

            SendMessages().Wait();            
            
            CloseConnection();
        }

        /// <summary>
        /// Method closes connection between client and server
        /// </summary>
        private void CloseConnection()
        {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        /// <summary>
        /// Method sends a message to the server
        /// </summary>
        /// <param name="message">Message text</param>
        private void SendMessage(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);

            int bytesSent = sender.Send(msg);
        }

        /// <summary>
        /// Method sends a messages to the server
        /// </summary>
        /// <returns>Running Task</returns>
        private Task SendMessages()
        {
            return Task.Run(() =>
            {
                string message = textGenerator.GenerateName();

                SendMessage(message);

                Console.WriteLine("My name is " + message);

                for(int i = 0; i < messagesCount; i++)
                {
                    Thread.Sleep(random.Next(0, delay));

                    message = textGenerator.GenerateMessage();
                    
                    SendMessage(message);
                    
                    Console.WriteLine("I:" + message);
                }
            });
        }

        /// <summary>
        /// Method receives a messages from the server
        /// </summary>
        /// <returns>Running Task</returns>
        private Task ReceiveMessages()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        byte[] receivedBytes = new byte[bufferSize];

                        StringBuilder builder = new StringBuilder();

                        int bytesCount = 0;

                        do
                        {
                            bytesCount = sender.Receive(receivedBytes);

                            builder.Append(Encoding.UTF8.GetString(receivedBytes, 0, bytesCount));
                        }
                        while (sender.Available > 0);

                        string message = builder.ToString();

                        Console.Write(message);
                    }
                    catch
                    {
                        Console.WriteLine("Server connection lost!");

                        break;
                    }
                }
            });
        }
    }
}
