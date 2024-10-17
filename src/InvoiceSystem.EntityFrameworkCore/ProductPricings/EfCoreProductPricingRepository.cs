using InvoiceSystem.EntityFrameworkCore;
using InvoiceSystem.ProductDiscounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InvoiceSystem.ProductPricings
{
    public class EfCoreProductPricingRepository : EfCoreRepository<InvoiceSystemDbContext, ProductPricing, Guid>, IProductPricingRepository
    {
        public EfCoreProductPricingRepository(IDbContextProvider<InvoiceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }
    }
}
