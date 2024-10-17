using InvoiceSystem.ProductDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InvoiceSystem.ProductPricings
{
    public interface IProductPricingRepository : IRepository<ProductPricing, Guid>
    {
    }
}
