using AutoMapper;
using OnlineStore.AccountServiceReference;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ClientController : Controller
    {
        IAccountSVC accountService;
        public ClientController(IAccountSVC serv)
        {
            accountService = serv;
        }

        [Authorize(Roles = "user, manager")]
        public ActionResult Index(string clientId)
        {
            IEnumerable<OrderHeaderDTO> orderHeaderDtos = accountService.GetClientOrderHeaders(clientId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderHeaderDTO, OrderHeaderViewModel>()).CreateMapper();
            var orderHeaders = mapper.Map<IEnumerable<OrderHeaderDTO>, List<OrderHeaderViewModel>>(orderHeaderDtos);

            return View(orderHeaders);
        }

    }
}