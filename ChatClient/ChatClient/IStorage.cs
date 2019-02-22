using System.Collections.Generic;

namespace ChatClient
{
    public interface IStorage
    {
        /// <summary>
        /// Method for names reading from some storage
        /// </summary>
        /// <returns>Collection of names</returns>
        List<string> ReadNames();

        /// <summary>
        /// Method for messages reading from some storage
        /// </summary>
        /// <returns>Collection of messages</returns>
        List<string> ReadMessages();
    }
}
