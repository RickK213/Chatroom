using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface ISubscriber
    {
        void Notify(User newUser);
        void Send(Message message);
        Message Recieve();
    }
}
