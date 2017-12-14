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
            string computerIPAddress = GetComputerIPAddress();
            Console.WriteLine("Local Computer IP Address: " + computerIPAddress);
            Console.WriteLine();
            server = new TcpListener(IPAddress.Parse(computerIPAddress), 9999);
            server.Start();
        }

        string GetComputerIPAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry host = Dns.GetHostEntry(hostName);
            string computerIPAddress = "127.0.0.1";
            foreach (var address in host.AddressList)
            {
                if (address.AddressFamily.ToString().Equals("InterNetwork"))
                {
                    computerIPAddress = address.ToString();
                }
            }
            return computerIPAddress;
        }

        public void Run()
        {
            while(true)
            {
                Parallel.Invoke(
                () =>
                {
                    Console.WriteLine("Begin first task...");
                    AcceptClient();
                    string message = client.Recieve();
                    Respond(message);
                },  // close first Action

                () =>
                {
                    Console.WriteLine("Begin second task...");

                }
            ); //close parallel.invoke
            }
            
            //string message = client.Recieve();
            //Respond(message);
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
