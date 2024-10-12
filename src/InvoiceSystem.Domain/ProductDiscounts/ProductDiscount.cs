using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.Products;
using Volo.Abp.Domain.Entities.Auditing;

namespace InvoiceSystem.ProductDiscounts
{
    public class ProductDiscount : FullAuditedEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Disount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
