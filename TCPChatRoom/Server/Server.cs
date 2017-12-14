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
    class Server
    {
        public static User client;
        TcpListener server;
        public Server()
        {

            string hostName = Dns.GetHostName();
            IPHostEntry host = Dns.GetHostEntry(hostName);
            //string computerIPAddress = host.AddressList[2].ToString();
            foreach(var item in host.AddressList)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
            //Console.WriteLine("Local Computer IP Address: " + computerIPAddress);
            Console.WriteLine();
            //server = new TcpListener(IPAddress.Parse(computerIPAddress), 9999);
            server.Start();
        }
        public void Run()
        {
            AcceptClient();
            string message = client.Recieve();
            Respond(message);
        }
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();
            Console.WriteLine("Connected");
            NetworkStream stream = clientSocket.GetStream();
            client = new User(stream, clientSocket);
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}
