using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InvoiceSystem.Invoices
{
    public interface IInvoiceRepository : IRepository<Invoice, Guid>
    {
        Task<Invoice> GetInvoice(Guid id);
        Task<List<Invoice>> GetAll(int skipCount,
            int maxResultCount,
            string sorting,
            int? filter);

    }
}
