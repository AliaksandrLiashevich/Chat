using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChatClient
{
    public class Storage: IStorage
    {
        private string namesPath = "../../Storage/Names";        
        private string messagesPath = "../../Storage/Messages";

        /// <summary>
        /// Method for names reading from some storage
        /// </summary>
        /// <returns>Collection of names</returns>
        public List<string> ReadNames()
        {
            List<string> names = new List<string>();

            using (StreamReader sr = new StreamReader(namesPath, Encoding.UTF8))
            {
                string name;

                while ((name = sr.ReadLine()) != null)
                {
                    names.Add(name);
                }
            }

            return names;
        }

        /// <summary>
        /// Method for messages reading from some storage
        /// </summary>
        /// <returns>Collection of messages</returns>
        public List<string> ReadMessages()
        {
            List<string> messages = new List<string>();

            using (StreamReader sr = new StreamReader(messagesPath, Encoding.UTF8))
            {
                string message;

                while ((message = sr.ReadLine()) != null)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }
    }
}
