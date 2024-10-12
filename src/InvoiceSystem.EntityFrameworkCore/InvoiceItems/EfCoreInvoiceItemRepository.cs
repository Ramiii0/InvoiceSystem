using InvoiceSystem.EntityFrameworkCore;
using InvoiceSystem.Invoices;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InvoiceSystem.InvoiceItems
{
    public class EfCoreInvoiceItemRepository : EfCoreRepository<InvoiceSystemDbContext, InvoiceItem, Guid>,IInvoiceItemRepository
    {
        public EfCoreInvoiceItemRepository(IDbContextProvider<InvoiceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
            

        }
       public async Task GetById(Guid id)
        {

        }
    }
}
