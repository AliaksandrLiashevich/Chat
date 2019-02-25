using System;
using System.Collections.Generic;

namespace ChatClient
{
    public class TextGenerator: ITextGenerator
    {
        private IStorage storage;

        private List<string> names;
        private List<string> messages;
        
        private Random random = new Random();

        public TextGenerator(IStorage _storage)
        {
            storage = _storage;
        }

        /// <summary>
        /// Method for random name generating
        /// </summary>
        /// <returns>String name</returns>
        public string GenerateName()
        {
            if (names == null)
            {
                names = storage.ReadNames();
            }

            return names[random.Next(0, names.Count - 1)];
        }

        /// <summary>
        /// Method for random message generating
        /// </summary>
        /// <returns>Message text</returns>
        public string GenerateMessage()
        {
            if (messages == null)
            {
                messages = storage.ReadMessages();
            }

            return messages[random.Next(0, messages.Count - 1)];
        }
    }
}
