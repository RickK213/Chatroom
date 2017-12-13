using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    //implement IObervable interface
    class Server : IObservable<TcpClient>
    {
        public static Client client; //Instead of 1 client, we need a dictionary of clients
        TcpListener server;
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Run()
        {
            AcceptClient(); //we need to accept a client every time a new client program instance starts to run
            //we need a loop here that uses concurrency to allow all users in the dictionary to send messages
                string message = client.Recieve();
                Respond(message);
            //end of loop ?
        }

        public IDisposable Subscribe(IObserver<TcpClient> observer)
        {
            //write this

        }

        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();
            Console.WriteLine("Connected");
            NetworkStream stream = clientSocket.GetStream();
            client = new Client(stream, clientSocket);
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}
