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
        //public static User user;
        public static Dictionary<int, User> users = new Dictionary<int, User>();
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
                //This thread is always listening for new clients (users)
                //TO DO: use a try catch here
                Parallel.Invoke(
                    () =>
                    {
                        AcceptUser();
                    }
                );
            }
            
            //string message = client.Recieve();
            //Respond(message);
        }

        private void AcceptUser()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient(); //this is blocking
            Console.WriteLine("Connected");
            NetworkStream stream = clientSocket.GetStream();
            User user = new User(stream, clientSocket);
            users.Add(user.UserId, user);
        }

        private void Respond(string body)
        {
             //user.Send(body);
        }
    }
}
