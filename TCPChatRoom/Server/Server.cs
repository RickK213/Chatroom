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
        //member variables
        Dictionary<int, User> users;
        TcpListener server;
        Queue<Message> messages;

        //constructor
        public Server()
        {
            messages = new Queue<Message>();
            users = new Dictionary<int, User>();
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
                //TO DO: use a try catch here
                Parallel.Invoke(
                    //This thread is always listening for new clients (users)
                    async () =>
                    {
                        await AcceptUser();
                    },
                    //This thread is always listening for new messages
                    async () =>
                    {
                        await GetAllMessages();
                    },
                    //This thread is always sending new messages
                    async () =>
                    {
                        await SendAllMessages();
                    }
                );

            }
        }

        Task SendAllMessages()
        {
            return Task.Run(() =>
            {
                if (messages.Count>0)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        Message currentMessage = messages.ElementAt(messages.Count - 1);
                        users.ElementAt(i).Value.Send(currentMessage);
                    }
                    messages.Dequeue();
                }
            }
            );
        }

        Task SendUserMessage(User user, Message message)
        {
            return Task.Run(() =>
            {
                user.Send(message);
            }
            );
        }

        Task GetAllMessages()
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < users.Count; i++)
                    {
                        Parallel.Invoke(
                            async () =>
                            {
                                await GetUserMessage(users.ElementAt(i).Value);
                            }
                        );
                    }
                }
            );
        }

        Task GetUserMessage(User user)
        {
            return Task.Run(() =>
            {
                Message message = user.Recieve();
                Console.WriteLine(message.Body);
                messages.Enqueue(message);
            }
            );
        }

        Task AcceptUser()
        {
            return Task.Run(() =>
                {
                    TcpClient clientSocket = default(TcpClient);
                    clientSocket = server.AcceptTcpClient();
                    Console.WriteLine("Connected");
                    NetworkStream stream = clientSocket.GetStream();                    
                    User user = new User(stream, clientSocket);
                    user.displayName = user.ReceiveDisplayName();
                    Console.WriteLine("{0} has joined the chat!",user.displayName);
                    users.Add(user.UserId, user);
                }
            );
        }

    }
}
