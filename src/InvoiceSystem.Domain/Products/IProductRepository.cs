using InvoiceSystem.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InvoiceSystem.Products
{
    public interface IProductRepository : IRepository<Product,Guid>
    {
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetAll(/*int skipCount,
            int maxResultCount,
            string sorting,
            int? filter*/);

    }
}

