using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class User : ISubscriber
    {
        NetworkStream stream;
        TcpClient client;
        public int UserId;
        public string displayName;

        public User(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            UserId = stream.GetHashCode();
            this.displayName = UserId.ToString();
        }

        public void Send(Message message)
        {
            byte[] messageBody = Encoding.ASCII.GetBytes(message.Body);
            stream.Write(messageBody, 0, messageBody.Count());
        }

        public Message Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
            Message message = new Message(this, recievedMessageString);
            //Console.WriteLine(recievedMessageString);
            return message;
        }
        public string ReceiveDisplayName()
        {
            byte[] receivedDisplayNameArray = new byte[256];
            stream.Read(receivedDisplayNameArray, 0, receivedDisplayNameArray.Length);
            int displayNameLength = 0;
            for(int i = 0; i < receivedDisplayNameArray.Length; i++)
            {
                if (receivedDisplayNameArray[i] != 0)
                {
                    displayNameLength++;
                }
            }
            byte[] displayNameArray = new byte[displayNameLength];
            Array.Copy(receivedDisplayNameArray, displayNameArray, displayNameLength);
            string receivedDisplayName = Encoding.ASCII.GetString(displayNameArray);
            return receivedDisplayName;
        }

        public void Notify(User newUser)
        {
            Message notification = new Message(newUser, "I've joined the chat!");
            Send(notification);

        }
    }
}
