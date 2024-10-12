using InvoiceSystem.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InvoiceSystem.Invoices
{
    public class EfCoreInvoiceRepository : EfCoreRepository<InvoiceSystemDbContext,Invoice,Guid>,IInvoiceRepository
    {
        
        public EfCoreInvoiceRepository(IDbContextProvider<InvoiceSystemDbContext> dbContextProvider) : base(dbContextProvider) 
        {
            
        }

        public async Task<Invoice> GetInvoice(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return  dbSet.Include(x => x.InvoiceItems).FirstOrDefault(x=>x.Id == id);
        }

        public async Task<List<Invoice>> GetAll(int skipCount, int maxResultCount, string sorting, int? filter )
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
           .WhereIf(
               filter != null, p => p.InvoiceNo == filter
               )
           .OrderBy(sorting)
           .Skip(skipCount)
           .Take(maxResultCount)
           .ToListAsync();

        }
    }
}
