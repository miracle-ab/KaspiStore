﻿using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Services;
using System.Collections.Generic;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IOrderProcessorService
    {
        void ProcessOrder(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails);
        int GetSalesPerson(string country);

    }
}
