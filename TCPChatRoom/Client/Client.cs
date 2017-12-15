using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        public Client(string IP, int port)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
        }
        Task Send()
        {
            return Task.Run(() =>
            {
                string messageString = UI.GetInput();
                byte[] message = Encoding.ASCII.GetBytes(messageString);
                stream.Write(message, 0, message.Count());
            });
        }
        Task Receive()
        {
            return Task.Run(() =>
            {
                byte[] recievedMessage = new byte[256];
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                UI.DisplayMessage(Encoding.ASCII.GetString(recievedMessage));
            });
        }

        public void Run()
        {
            while (true)
            {
                Parallel.Invoke(
                    //This thread is always allowing the client to send new messages
                    async () =>
                    {
                        await Send();
                    },
                    //This thread is always listening for new messages
                    async () =>
                    {
                        await Receive();
                    }
                );

            }
        }
    }
}
