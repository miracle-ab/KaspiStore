using MimeKit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Infrastructure.Business.Enums;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Business.Interfaces;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class EmailService : IEmailService
    {
        UnitOfWork unitOfWork { get; set; }

        public EmailService()
        {
            unitOfWork = new UnitOfWork();
        }

        public async Task SendEmailAsync(int orderHeaderId)
        {
            var orderHeader = unitOfWork.PurchaseOrderHeader.Get(orderHeaderId);
            var customer = unitOfWork.Person.Get((int)orderHeader.PersonID);

            StringBuilder body = new StringBuilder()
                    .AppendLine("Status of your order")
                    .AppendLine("<br />")
                    .AppendLine("Products: <br />");

            foreach (var line in orderHeader.PurchaseOrderDetails)
            {
                var subtotal = line.UnitPrice * line.OrderQty;
                body.AppendFormat("{0} x {1} (total: {2:c}) <br />",
                    line.OrderQty, line.Product.Name, subtotal);
            }

            body.AppendFormat("<br />Total cost: {0:c}", orderHeader.TotalDue)
                .AppendLine("<br />")
                .AppendLine("Shipment:")
                .AppendLine(customer.FirstName + " " + customer.LastName)
                .AppendLine(customer.BusinessEntity.BusinessEntityAddresses.First().Address.City)
                .AppendLine(customer.BusinessEntity.BusinessEntityAddresses.First().Address.AddressLine1)
                .AppendLine(customer.PersonPhones.First().PhoneNumber)
                .AppendLine("<br />")
                .AppendLine("Status:")
                .AppendLine($"<strong>{((Status)orderHeader.Status).ToString()}</strong>");


            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("OnlineStore", "login@onlinestore.company"));
            emailMessage.To.Add(new MailboxAddress("", customer.EmailAddresses.First().EmailAddress1));
            emailMessage.Subject = "Order Info";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body.ToString()
            };

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("storeemailbotservice@gmail.com", "e3E@{,xxnCs;B5'K");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
