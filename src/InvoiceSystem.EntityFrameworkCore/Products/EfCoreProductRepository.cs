using InvoiceSystem.EntityFrameworkCore;
using InvoiceSystem.Invoices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InvoiceSystem.Products
{
    public class EfCoreProductRepository : EfCoreRepository<InvoiceSystemDbContext, Product, Guid>, IProductRepository
    {
        public EfCoreProductRepository(IDbContextProvider<InvoiceSystemDbContext> dbContextProvider) : base(dbContextProvider) 
        {
            
        }
        public async Task<List<Product>> GetAll()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Include(x => x.ProductPricing).Include(z=>z.ProductDiscount).ToListAsync();


        }

        public async Task<Product> GetProduct(Guid id)
        {
            var dbSet = await GetDbSetAsync();
          
            return dbSet.Include(x => x.ProductPricing).Include(x=>x.ProductDiscount).FirstOrDefault(a=>a.Id == id);
        }
    }
}
