using InvoiceSystem.EntityFrameworkCore;
using InvoiceSystem.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InvoiceSystem.ProductDiscounts
{
    public class EfCoreProductDiscountRepository : EfCoreRepository<InvoiceSystemDbContext, ProductDiscount, Guid>, IProductDiscountRepository
    {
        public EfCoreProductDiscountRepository(IDbContextProvider<InvoiceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
