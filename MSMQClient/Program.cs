using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new MessageQueue(@".\private$\OrdersQueue")
            {
                Formatter = new XmlMessageFormatter(new String[] { "System.String" })
            };

            foreach (Message message in queue.GetAllMessages())
            {
                Console.WriteLine(message.Label);
                Console.WriteLine(message.Body);
                Console.WriteLine("------------------------");
            }

            Console.ReadKey();
        }
    }
}
