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
            //Use Parallel.Invode to allow for multiple connections to the server
            //Original Code: msdn.microsoft.com/en-us/library/dd992634(v=vs.110).aspx
        //    while (true)
        //    {
        //        try
        //        {
        //            Parallel.Invoke(AcceptClient,    // Param #0 - static method
        //                () =>           // Param #1 - lambda expression
        //            {
        //                    Console.WriteLine("Method=beta, Thread={0}", Thread.CurrentThread.ManagedThreadId);
        //                },
        //                delegate ()     // Param #2 - in-line delegate
        //            {
        //                    Console.WriteLine("Method=gamma, Thread={0}", Thread.CurrentThread.ManagedThreadId);
        //                }
        //            );
        //        }
        //        // No exception is expected in this example, but if one is still thrown from a task,
        //        // it will be wrapped in AggregateException and propagated to the main thread.
        //        catch (AggregateException e)
        //        {
        //            Console.WriteLine("An action has thrown an exception. THIS WAS UNEXPECTED.\n{0}", e.InnerException.ToString());
        //        }






        //    }
        //    string message = client.Recieve();
        //    Respond(message);
        //}

        public void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient(); //this is blocking
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
