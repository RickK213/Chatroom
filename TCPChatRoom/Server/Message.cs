using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Message
    {
        public User sender;
        public string Body;
        public int UserId;
        public Message(User Sender, string Body)
        {
            sender = Sender;
            this.Body = Body;
            UserId = sender.UserId;
        }
    }
}
