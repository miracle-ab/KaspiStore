using System.Xml.Linq;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IMSMQService
    {
        void SendMessage(XDocument xmlDoc, int purchaseOrderHeaderID);   
    }
}
