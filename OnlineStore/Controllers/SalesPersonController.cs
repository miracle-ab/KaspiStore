using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OnlineStore.Models.ViewModels;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using Microsoft.AspNet.Identity;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Domain.Core.Entities;

namespace OnlineStore.Controllers
{
    public class SalesPersonController : Controller
    {
        ISalesPersonService salesPersonService;
        public SalesPersonController(ISalesPersonService serv)
        {
            salesPersonService = serv;
        }

        [Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            IEnumerable<OrderHeaderDTO> orderHeaderDtos = salesPersonService.GetOrderHeaders(userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderHeaderDTO, OrderHeaderViewModel>()).CreateMapper();
            var orderHeaders = mapper.Map<IEnumerable<OrderHeaderDTO>, List<OrderHeaderViewModel>>(orderHeaderDtos);

            return View(orderHeaders);
        }
    }
}
