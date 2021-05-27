using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineStore.Models.ViewModels;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.SalesPersonServiceReference;

namespace OnlineStore.Controllers
{
    public class SalesPersonController : Controller
    {
        ISalesPersonSVC salesPersonSVC;
        public SalesPersonController(ISalesPersonSVC serv)
        {
            salesPersonSVC = serv;
        }

        [Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            IEnumerable<OrderHeaderDTO> orderHeaderDtos = salesPersonSVC.GetOrderHeaders(userId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderHeaderDTO, OrderHeaderViewModel>()).CreateMapper();
            var orderHeaders = mapper.Map<IEnumerable<OrderHeaderDTO>, List<OrderHeaderViewModel>>(orderHeaderDtos);

            return View(orderHeaders);
        }

        [Authorize(Roles = "manager")]
        public ActionResult AllOrderHeaders()
        {
            IEnumerable<OrderHeaderDTO> allOrderHeaderDtos = salesPersonSVC.GetAllOrderHeaders();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderHeaderDTO, OrderHeaderViewModel>()).CreateMapper();
            var orderHeaders = mapper.Map<IEnumerable<OrderHeaderDTO>, List<OrderHeaderViewModel>>(allOrderHeaderDtos);

            return View(orderHeaders);
        }


        [Authorize(Roles = "manager")]
        public ActionResult OrderDetails(int purchaseOrderHeaderID)
        {

            SalesProductDTO salesProductDTO = salesPersonSVC.GetOrderDetails(purchaseOrderHeaderID);

            var mapperProduct = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapperProduct.Map<List<ProductDTO>, List< ProductViewModel>>(salesProductDTO.Products);

            var mapperCustomer = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>()).CreateMapper();
            var customer = mapperCustomer.Map<CustomerDTO, CustomerViewModel>(salesProductDTO.Customer);

            return View(new SalesProductViewModel 
            {
                Products = products,
                Customer = customer
            });
        }


        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult SentForShipment(int purchaseOrderHeaderID, string returnUrl)
        {
            salesPersonSVC.CreateShipmentXML(purchaseOrderHeaderID);

            return RedirectToAction("Index");
        }
    }
}
