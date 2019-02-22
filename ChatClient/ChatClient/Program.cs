using System;

namespace ChatClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Client client = new Client(new TextGenerator(new Storage()));

                client.StartWorking();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
