using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(int orderHeaderId);
    }
}
