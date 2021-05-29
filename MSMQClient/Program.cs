using OnlineStore.Infrastructure.Business.Services;
using OnlineStore.Infrastructure.Data;
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

            UnitOfWork unitOfWork = new UnitOfWork();

            var salesPersonService = new SalesPersonService(unitOfWork);

            foreach (Message message in queue.GetAllMessages())
            {
                var id = int.Parse(message.Label);
                salesPersonService.ChangeOrderStatus(id);

                Console.WriteLine(message.Label);
                Console.WriteLine(message.Body);
                Console.WriteLine("------------------------");
            }

            queue.Purge();
            Console.ReadKey();
        }
    }
}
