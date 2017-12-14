using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class User
    {
        NetworkStream stream;
        TcpClient client;
        public int UserId;

        public User(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            UserId = stream.GetHashCode();
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

    }
}
