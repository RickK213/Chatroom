using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Message
    {
        //member variables
        public User sender;
        public string Body;
        public int UserId;

        //constructor
        public Message(User Sender, string Body)
        {
            sender = Sender;
            StringBuilder wholeMessage = new StringBuilder();
            wholeMessage.Append(sender.displayName);
            wholeMessage.Append(": ");
            wholeMessage.Append(Body);
            this.Body = wholeMessage.ToString();
            UserId = sender.UserId;
        }
    }
}
