using System;
using System.Messaging;
using OnlineStore.Infrastructure.Business.Interfaces;
using System.Xml.Linq;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class MSMQService : IMSMQService
    {
        public void SendMessage(XDocument xmlDoc, int purchaseOrderHeaderID)
        {
            try
            {
                if (!MessageQueue.Exists(@".\private$\OrdersQueue"))
                {
                    MessageQueue.Create(@".\private$\OrdersQueue");
                }

                var messageQueue = new MessageQueue(@".\private$\OrdersQueue");

                messageQueue.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(System.String) });

                messageQueue.Send(xmlDoc.ToString(), $"OrderNumber({purchaseOrderHeaderID})");
            }
            catch (MessageQueueException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
