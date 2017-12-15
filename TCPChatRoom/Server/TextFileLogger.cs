using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    class TextFileLogger : ILoggable
    {

        public void Save(Message message)
        {
            StreamWriter sw = File.AppendText(@"ChatLog.txt");
            try
            {
                string logLine = String.Format("{0:G}: {1}.", DateTime.Now, message.Body);
                sw.WriteLine(logLine); 
            }
            finally
            {
                sw.Close();
            }
        }

    }
}
