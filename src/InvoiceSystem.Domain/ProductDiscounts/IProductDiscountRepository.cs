﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InvoiceSystem.ProductDiscounts
{
    public interface IProductDiscountRepository : IRepository<ProductDiscount,Guid >
    {
    }
}
