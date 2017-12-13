using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the IP Address for the Server Computer");
            string serverIPAddress = Console.ReadLine();

            Client client = new Client(serverIPAddress, 9999);
            while(true)
            {
                client.Send();
                client.Recieve();
            }
            Console.ReadLine();
        }
    }
}
