using System.Net.Sockets;

namespace ChatServer
{
    public class ClientInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Socket Socket { get; set; }

        /// <summary>
        /// Constructor that initializes object described by id, name and socket
        /// </summary>
        /// <param name="_id">Unique number that identifies client</param>
        /// <param name="_name">Name of clietn</param>
        /// <param name="_socket">Object that handle connection between server and client</param>
        public ClientInfo(int _id, string _name, Socket _socket)
        {
            Id = _id;

            Name = _name;

            Socket = _socket;
        }

        /// <summary>
        /// Method compares objects for similarity
        /// </summary>
        /// <param name="obj">Instance for comparison</param>
        /// <returns>True(objects are similar) or False(objects are different)</returns>
        public override bool Equals(object obj)
        {
            ClientInfo clientInfo = obj as ClientInfo;

            if (clientInfo != null)
            {
                return clientInfo.Id.Equals(Id) && clientInfo.Name.Equals(Name);
            }

            return false;
        }

        /// <summary>
        /// A quick way to make sure that objects are different
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
